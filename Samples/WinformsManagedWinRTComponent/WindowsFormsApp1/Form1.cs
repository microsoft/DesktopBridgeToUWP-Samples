//*********************************************************
//
// Copyright (c) Microsoft. All rights reserved.
// This code is licensed under the MIT License (MIT).
// THIS CODE IS PROVIDED *AS IS* WITHOUT WARRANTY OF
// ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING ANY
// IMPLIED WARRANTIES OF FITNESS FOR A PARTICULAR
// PURPOSE, MERCHANTABILITY, OR NON-INFRINGEMENT.
//
//*********************************************************
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SimpleMathManagedWinRT;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public enum Operation
        {
            Add,
            Subtract,
            Mulitply,
            Divide,
            Equals
        }

        private double num1=0;
        private double num2=0;
        private double result=0;
        private Operation op;
        private bool clearOnClickFlag = false;

        public Form1()
        {
            InitializeComponent();
        }

        void operationButton_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (btn.Text.Contains("="))
            {
                double.TryParse(textboxRegister.Text, out num2);
                //call into math function
                Evaluate();
            }
            else
            {
                double.TryParse(textboxRegister.Text, out num1);
                textboxRegister.Text = string.Empty;
                switch (btn.Text)
                {
                    case "+":
                        op = Operation.Add;
                        break;
                    case "-":
                        op = Operation.Subtract;
                        break;
                    case "*":
                        op = Operation.Mulitply;
                        break;
                    case "/":
                        op = Operation.Divide;
                        break;
                }
            }
        }

        void Evaluate()
        {
            //Call the SimpleMath functions in the WinRT Component:
            var sm = new SimpleMathManagedWinRT.SimpleMath();
            
            switch (op)
            {
                case Operation.Add:
                    //result = num1 + num2;
                    result = sm.add(num1, num2);
                    break;
                case Operation.Subtract:
                    //result = num1 - num2;
                    result = sm.subtract(num1, num2);
                    break;
                case Operation.Mulitply:
                    //result = num1 * num2;
                    result = sm.multiply(num1, num2);
                    break;
                case Operation.Divide:
                    if (num2 != 0)
                    {
                        //result = (num1 / num2);
                        result = sm.divide(num1, num2);
                    }
                    else
                    {
                        msgLabel.Text = "Divide by zero";
                    }
                    break;
            }
            textboxRegister.Text = result.ToString();
            clearOnClickFlag = true;


        }
        void numericButton_Click(object sender, EventArgs e)
        {
            if (clearOnClickFlag)
            {
                ClearAll();
            }

            try
            {
                Button btn = sender as Button;

                if (!btn.Text.Contains("."))
                {
                    textboxRegister.Text += btn.Text;
                }
                else
                {
                    if (!textboxRegister.Text.Contains("."))
                    {
                        textboxRegister.Text += ".";
                    }
                }
            }
            catch (Exception ex)
            {
                msgLabel.Text = "Unexpected error: " + ex.Message;
            }
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            ClearAll();
        }
        private void ClearAll()
        {
            textboxRegister.Text = string.Empty;
            num1 = 0;
            num2 = 0;
            clearOnClickFlag = false;
        }

    }
}

