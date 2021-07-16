#include "pch.h"

#define CLSID_SampleTask "1498AA14-E11E-48C8-912A-BCEBF27E19AC"

const char* RegisterProcessForComTypeToken = "-RegisterProcessAsComServer";
const char* RegisterTimeTriggeredSampleTaskToken = "-RegisterTimeTriggeredSampleTask";
const hstring TimeTriggeredSampleTaskName(L"TimeTriggeredSampleTask");

namespace Win32BackgroundExecutionApiSample {

    handle _taskFactoryCompletionEvent{ CreateEvent(nullptr, false, false, nullptr) };

    struct __declspec(uuid(CLSID_SampleTask))
        SampleTask  : implements<SampleTask, IBackgroundTask>
    {
        const unsigned int MaximumPotentialPrime = 1000000000;
        volatile bool IsCanceled = false;
        BackgroundTaskDeferral TaskDeferral = nullptr;

        unsigned int GetNextPrime (_In_ unsigned int lastPrime)
        {
            unsigned int nextPrime = lastPrime;
            unsigned int divisor;
            while (TRUE)
            {
                divisor = nextPrime;
                nextPrime += 1;
                while (divisor != 1)
                {
                    if ((nextPrime % divisor) == 0) {
                        break;
                    }

                    divisor -= 1;
                }

                if (divisor == 1)
                {
                    break;
                }
            }

            return nextPrime;
        }

        void __stdcall Run (_In_ IBackgroundTaskInstance taskInstance)
        {
            printf("SampleTask::Run\n");

            taskInstance.Canceled({ this, &SampleTask::OnCanceled });

            TaskDeferral = taskInstance.GetDeferral();

            unsigned int currentPrimeNumber = 1;
            while (!IsCanceled && (currentPrimeNumber < MaximumPotentialPrime))
            {
                currentPrimeNumber = GetNextPrime(currentPrimeNumber);
            }

            printf("SampleTask::Completed on prime==%ul\n", currentPrimeNumber);
            check_bool(SetEvent(_taskFactoryCompletionEvent.get()));
            TaskDeferral.Complete();
        }

        void __stdcall OnCanceled (_In_ IBackgroundTaskInstance /* taskInstance */, _In_ BackgroundTaskCancellationReason /* cancelReason */)
        {
            printf("SampleTask::OnCanceled\n");
            IsCanceled = true;
        }
    }; // class SampleTask

    struct TaskFactory : implements<TaskFactory, IClassFactory>
    {
        HRESULT __stdcall CreateInstance (_In_opt_ IUnknown* aggregateInterface, _In_ REFIID interfaceId, _Outptr_ VOID** object) noexcept final
        {
            if (aggregateInterface != NULL) {
                return CLASS_E_NOAGGREGATION;
            }

            return make<SampleTask>().as(interfaceId, object);
        }

        HRESULT __stdcall LockServer (BOOL lock) noexcept final
        {
            UNREFERENCED_PARAMETER(lock);
            return S_OK;
        }
    };

    class RegisterProcessForComType
    {
    private:
        DWORD ComRegistrationToken = 0;

    public:
        ~RegisterProcessForComType()
        {
            if (ComRegistrationToken != 0)
            {
                CoRevokeClassObject(ComRegistrationToken);
            }
        }

        hresult RegisterAndBlock(guid classId)
        {
            hresult hr;
            try
            {
                com_ptr<IClassFactory> taskFactory = make<TaskFactory>();

                check_hresult(CoRegisterClassObject(classId,
                                                    taskFactory.get(),
                                                    CLSCTX_LOCAL_SERVER,
                                                    REGCLS_MULTIPLEUSE,
                                                    &ComRegistrationToken));

                printf("Press any character to stop handling new tasks...\n");
                auto dummyCharacter = getchar();
                hr = S_OK();

            }
            catch (...)
            {
                hr = to_hresult();
            }

            return hr;
        }

        hresult RegisterAndWaitSingle(guid classId)
        {
            hresult hr;
            try
            {
                // Verify the global handle is correctly constructed.
                check_bool(bool { _taskFactoryCompletionEvent });

                com_ptr<IClassFactory> taskFactory = make<TaskFactory>();

                check_hresult(CoRegisterClassObject(classId,
                                                    taskFactory.get(),
                                                    CLSCTX_LOCAL_SERVER,
                                                    REGCLS_MULTIPLEUSE,
                                                    &ComRegistrationToken));

                check_hresult(WaitForSingleObject(_taskFactoryCompletionEvent.get(),
                                                  INFINITE));

                hr = S_OK;
            }
            catch (...)
            {
                hr = to_hresult();
            }

            return hr;
        }
    }; // class RegisterProcessForComType

    void UnregisterDuplicateTask (_In_ hstring taskName)
    {
        auto allRegistrations = BackgroundTaskRegistration::AllTasks();
        for (auto taskPair : allRegistrations)
        {
            IBackgroundTaskRegistration task = taskPair.Value();
            if (taskName == task.Name())
            {
                task.Unregister(true);
            }
        }
    }

    hresult RegisterTask (_In_ IBackgroundTrigger trigger, _In_ hstring taskName, _In_ guid classId, _In_ bool UnregisterOldTask = false)
    {
        hresult hr;

        try
        {
            if (UnregisterOldTask)
            {
                UnregisterDuplicateTask(taskName);
            }

            BackgroundTaskBuilder taskBuilder;
            taskBuilder.SetTrigger(trigger);
            taskBuilder.Name(taskName);
            taskBuilder.SetTaskEntryPointClsid(classId);

            taskBuilder.Register();
        }
        catch (...)
        {
            hr = to_hresult();
        }

        return hr;
    }

} // namespace Win32BackgroundExecutionApiSample

using namespace Win32BackgroundExecutionApiSample;

int main (_In_ int argc, _In_reads_(argc) const char** argv)
{
    hresult hr;

    init_apartment();

    if (argc <= 1)
    {
        hr = E_INVALIDARG;
        goto Exit;
    }

    if (_strnicmp(RegisterProcessForComTypeToken,
                  argv[1],
                  strlen(RegisterProcessForComTypeToken)) == 0)
    {
        RegisterProcessForComType registerForCom;

        // Use RegisterAndWaitSingle if you want this process to handle a single
        // a background task requests and then exit.
        // hr = registerForCom.RegisterAndWaitSingle(__uuidof(SampleTask));

        // Use RegisterAndBlock if you want this process to handle all future
        // background task requests until this process is terminated or exits
        // for some other reason.
        hr = registerForCom.RegisterAndBlock(__uuidof(SampleTask));
        if (FAILED(hr))
        {
            goto Exit;
        }
    }
    else if (_strnicmp(RegisterTimeTriggeredSampleTaskToken,
                       argv[1],
                       strlen(RegisterTimeTriggeredSampleTaskToken)) == 0)
    {
        hr = RegisterTask(TimeTrigger(15, false), TimeTriggeredSampleTaskName, __uuidof(SampleTask), true);
        if (FAILED(hr))
        {
            goto Exit;
        }
    }
    else
    {
        hr = RegisterTask(TimeTrigger(15, false), TimeTriggeredSampleTaskName, __uuidof(SampleTask));
        if (FAILED(hr))
        {
            goto Exit;
        }
    }

    hr = S_OK;

Exit:
    // Request input to stop command window from exiting before results are observed.
    printf("Result = 0x%08x\n", hr.value);
    printf("Press any character to exit...");
    auto dummyCharacter = getchar();

    return (int)hr;
}
