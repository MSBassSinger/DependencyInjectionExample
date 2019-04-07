using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace DependencyInjectionExample
{
    public static class CommonLib
    {

        /// <summary>
        /// Returns error messages from the parent exception and any 
        /// exceptions down the stack, and optionally, the data collection.
        /// 
        /// </summary>
        /// <param name="ex2Examine">The exception to examine.</param>
        /// <param name="getDataCollection">True if the data collection items are to be included; False if not.</param>
        /// <param name="getStackTrace">True if the stack trace is to be included; False if not.</param>
        /// <returns>A string with the error messages</returns>
        public static String GetFullExceptionMessage(this Exception ex2Examine,
                                                     Boolean getDataCollection,
                                                     Boolean getStackTrace)
        {

            String retValue = "";
            String message = "";
            String data = "";
            String stackTrace = "";


            try
            {

                if (((ex2Examine != null)))
                {

                    if ((getStackTrace))
                    {

                        if (((ex2Examine.StackTrace != null)))
                        {
                            stackTrace = "; Stack Trace=[" + ex2Examine.StackTrace + "].";
                        }

                    }

                    Exception nextException = ex2Examine;

                    message = "";

                    while (nextException != null)
                    {

                        data = "";

                        message += nextException.Message ?? "NULL";


                        if (nextException.Source != null)
                        {
                            message += "; Source=[" + nextException.Source + "]";

                        }


                        if (getDataCollection)
                        {
                            if (nextException.Data != null)
                            {
                                if (nextException.Data.Count > 0)
                                {
                                    foreach (DictionaryEntry item in nextException.Data)
                                    {
                                        data += "{" + item.Key.ToString() + "}={" + item.Value.ToString() + "}|";
                                    }

                                    data = data.Substring(0, data.Length - 1);
                                }

                            }

                        }

                        if ((data.Length > 0))
                        {
                            message = message + "; Data=[" + data + "]";
                        }

                        if (nextException.InnerException == null)
                        {
                            break;
                        }
                        else
                        {
                            nextException = nextException.InnerException;
                        }

                        message += "::";

                    }


                    if ((stackTrace.Length > 0))
                    {
                        message += "; " + stackTrace;
                    }

                }

                retValue = message.Trim();

                if (retValue.EndsWith("::"))
                {
                    retValue = retValue.Substring(0, retValue.Length - 2);
                }

            }
            catch (Exception exUnhandled)
            {
                exUnhandled.Data.Add("getDataCollection", getDataCollection.ToString());

                exUnhandled.Data.Add("getStackTrace", getStackTrace.ToString());

                throw;

            }

            return retValue;

        }

        public static Boolean Contains(this String source, String toCheck, StringComparison strComp)
        {

            Boolean retVal = false;

            try
            {

                if (toCheck != null)
                {
                    if (toCheck.Length > 0)
                    {
                        retVal = (source?.IndexOf(toCheck, strComp) >= 0);
                    }
                }

            }
            catch (Exception exUnhandled)
            {
                exUnhandled.Data.Add("source", source ?? "NULL");
                exUnhandled.Data.Add("toCheck", toCheck ?? "NULL");
                exUnhandled.Data.Add("strComp", strComp.ToString());

                throw;

            }

            return retVal;

        }  // END public static Boolean Contains(this String source, String toCheck, StringComparison strComp)

        public static string Replace(this String origString, String value2Replace, String newValue, StringComparison strComp)
        {
            StringBuilder sb = new StringBuilder();

            Int32 previousIndex = 0;

            Int32 index = 0;

            String retVal = "";

            try
            {
                if (value2Replace != null)
                {
                    if (newValue == null)
                    {
                        newValue = "";
                    }

                    index = origString.IndexOf(value2Replace, strComp);

                    while (index != -1)
                    {
                        sb.Append(origString.Substring(previousIndex, index - previousIndex));
                        sb.Append(newValue);
                        index += value2Replace.Length;

                        previousIndex = index;
                        index = origString.IndexOf(value2Replace, index, strComp);
                    }

                    sb.Append(origString.Substring(previousIndex));

                    retVal = sb.ToString();

                } // END if (value2Replace != null)

            }
            catch (Exception exUnhandled)
            {
                exUnhandled.Data.Add("origString", origString ?? "NULL");
                exUnhandled.Data.Add("value2Replace", value2Replace ?? "NULL");
                exUnhandled.Data.Add("newValue", newValue ?? "NULL");
                exUnhandled.Data.Add("strComp", strComp.ToString());

                throw;

            }
            finally
            {
                if (sb != null)
                {
                    sb.Clear();

                    sb = null;
                }
            }

            return retVal;

        }  // END public static string Replace(this String origString, String value2Replace, String newValue, StringComparison strComp)

        /// <summary>
        /// Converts a string, assumed to be in a format that can be converted, to a Boolean value.
        /// 
        /// If the conversion fails, the default value is returned.
        /// </summary>
        /// <param name="valueToExamine">The string value to be converted, that also host this method.</param>
        /// <param name="isBoolean">Out parameter that is True if it can be converted to Boolean, and False if not.</param>
        /// <returns>Returns the Boolean value, and whether the conversion was successful by out parameter.</returns>
        public static Boolean ConvertToBoolean(this String valueToExamine, out Boolean isBoolean)
        {


            Boolean retVal = false;

            isBoolean = false;

            try
            {

                if (!IsBoolean(valueToExamine))
                {
                    isBoolean = false;
                }
                else
                {

                    if ((!Boolean.TryParse(valueToExamine, out retVal)))
                    {

                        valueToExamine = valueToExamine.Trim().ToLower();

                        if (valueToExamine.IsOnlyDigits())
                        {
                            Int64 nValue = 0;

                            if (Int64.TryParse(valueToExamine, out nValue))
                            {
                                if (nValue == 0)
                                {
                                    retVal = false;
                                    isBoolean = true;
                                }
                                else
                                {
                                    retVal = true;
                                    isBoolean = true;
                                }
                            }
                            else
                            {
                                isBoolean = false;
                            }
                        }
                        else
                        {

                            switch ((valueToExamine))
                            {

                                case "on":
                                case "yes":
                                case "up":
                                case "ok":
                                case "1":
                                case "-1":
                                    retVal = true;
                                    isBoolean = true;
                                    break;
                                case "off":
                                case "no":
                                case "down":
                                case "not ok":
                                case "0":
                                    retVal = false;
                                    isBoolean = true;
                                    break;
                                default:
                                    isBoolean = false;
                                    break;
                            }  // END switch ((valueToExamine))
                        }
                    }  // END if ((!Boolean.TryParse(valueToExamine, out retVal)))
                    else
                    {
                        isBoolean = true;
                    }
                }

            }
            catch (Exception exUnhandled)
            {
                exUnhandled.Data.Add("valueToExamine", valueToExamine ?? "NULL");
                throw;

            }

            return retVal;

        }


        /// <summary>
        /// Tests a string, assumed to be in a format that can be converted, to a Boolean value.
        /// 
        /// If the conversion fails, False is returned.  Otherwise, True is returned.
        /// </summary>
        /// <param name="valueToExamine">The string value to be converted, that also host this method.</param>
        /// <returns>Returns true if it can be converted, false if not.</returns>
        public static Boolean IsBoolean(this String valueToExamine)
        {


            Boolean retVal = false;


            try
            {

                if ((String.IsNullOrWhiteSpace(valueToExamine)))
                {
                    retVal = false;
                }
                else
                {

                    if ((!Boolean.TryParse(valueToExamine, out retVal)) || valueToExamine == "0")
                    {

                        valueToExamine = valueToExamine.Trim().ToLower();

                        if (valueToExamine.IsOnlyDigits())
                        {
                            Int64 nValue = 0;

                            if (Int64.TryParse(valueToExamine, out nValue))
                            {
                                if (nValue == 0)
                                {
                                    retVal = false;
                                }
                                else
                                {
                                    retVal = true;
                                }
                            }
                            else
                            {
                                retVal = false;
                            }
                        }
                        else
                        {

                            switch ((valueToExamine))
                            {

                                case "on":
                                case "yes":
                                case "up":
                                case "ok":
                                case "1":
                                case "-1":
                                    retVal = true;
                                    break;
                                case "off":
                                case "no":
                                case "down":
                                case "not ok":
                                case "0":
                                    retVal = false;
                                    break;
                                default:
                                    retVal = false;
                                    break;
                            }
                        }
                    }
                }

            }
            catch (Exception exUnhandled)
            {
                exUnhandled.Data.Add("valueToExamine", valueToExamine ?? "NULL");
                throw;

            }

            return retVal;

        }  // END public static Boolean IsBoolean(this String valueToExamine)

        /// <summary>
        /// This process checks for all characters being digits.  Conversion
        /// functions to test numbers may translate letters as Hex values.
        /// </summary>
        /// <param name="testString"></param>
        /// <returns>True if the string is only digits, False if not.</returns>
        public static Boolean IsOnlyDigits(this String testString)
        {


            Boolean retValue = true;


            try
            {
                // remove white spaces 
                testString = testString.Trim();


                if ((testString.Length == 0))
                {
                    retValue = false;
                }

                // We have a string with no spaces.

                if ((retValue))
                {
                    // Loop through the string, checking each character.
                    for (Int32 index = 0; index <= testString.Length - 1; index++)
                    {
                        if ((char.IsDigit(testString[index]) == false))
                        {
                            retValue = false;
                            break;
                        }

                    }

                }

            }
            catch (Exception exUnhandled)
            {
                exUnhandled.Data.Add("testString", testString ?? "NULL");

                throw;

            }

            return retValue;
        }


        /// <summary>
        /// This process gets all digits in a string
        /// </summary>
        /// <param name="testString"></param>
        /// <returns>String of digits.</returns>
        public static String GetOnlyDigits(this String testString)
        {


            String retValue = "";


            try
            {
                // remove white spaces 
                testString = testString ?? "";

                testString = testString.Trim();


                if ((testString.Length == 0))
                {
                    retValue = "";
                }
                else
                {
                    // Loop through the string, checking each character.
                    for (Int32 index = 0; index <= testString.Length - 1; index++)
                    {
                        if ((char.IsDigit(testString[index]) == true))
                        {
                            retValue += testString[index];
                        }

                    }

                }

            }
            catch (Exception exUnhandled)
            {
                exUnhandled.Data.Add("testString", testString ?? "NULL");

                throw;

            }

            return retValue;
        }

        /// <summary>
        /// This process gets all letters in a string
        /// </summary>
        /// <param name="testString"></param>
        /// <returns>String of digits.</returns>
        public static String GetOnlyLetters(this String testString)
        {


            String retValue = "";


            try
            {
                // remove white spaces 
                testString = testString ?? "";

                testString = testString.Trim();


                if ((testString.Length == 0))
                {
                    retValue = "";
                }
                else
                {
                    // Loop through the string, checking each character.
                    for (Int32 index = 0; index <= testString.Length - 1; index++)
                    {
                        if ((char.IsLetter(testString[index]) == true))
                        {
                            retValue += testString[index];
                        }

                    }

                }

            }
            catch (Exception exUnhandled)
            {
                exUnhandled.Data.Add("testString", testString ?? "NULL");

                throw;

            }

            return retValue;
        }


        /// <summary>
        /// This process gets all letters and digits in a string
        /// </summary>
        /// <param name="testString"></param>
        /// <returns>String of digits.</returns>
        public static String GetOnlyLettersAndDigits(this String testString)
        {


            String retValue = "";


            try
            {
                // remove white spaces 
                testString = testString ?? "";

                testString = testString.Trim();


                if ((testString.Length == 0))
                {
                    retValue = "";
                }
                else
                {
                    // Loop through the string, checking each character.
                    for (Int32 index = 0; index <= testString.Length - 1; index++)
                    {
                        if ((char.IsLetterOrDigit(testString[index]) == true))
                        {
                            retValue += testString[index];
                        }

                    }

                }

            }
            catch (Exception exUnhandled)
            {
                exUnhandled.Data.Add("testString", testString ?? "NULL");

                throw;

            }

            return retValue;
        }


        /// <summary>
        /// This process checks for all characters being letters.
        /// </summary>
        /// <param name="testString"></param>
        /// <returns>True if the string is only letters, False if not.</returns>
        public static Boolean IsOnlyLetters(this String testString)
        {


            Boolean retValue = true;


            try
            {
                //'remove white spaces 
                testString = testString.Trim();


                if ((testString.Length == 0))
                {
                    retValue = false;
                }

                // We have a string with no spaces.

                if ((retValue))
                {
                    //' Loop through the string, checking each character.
                    for (Int32 index = 0; index <= testString.Length - 1; index++)
                    {
                        if ((char.IsLetter(testString[index]) == false))
                        {
                            retValue = false;
                            break;
                        }

                    }

                }

            }
            catch (Exception exUnhandled)
            {
                exUnhandled.Data.Add("testString", testString ?? "NULL");

                throw;

            }

            return retValue;
        }

        /// <summary>
        /// This process checks for all characters being only letters and numbers.
        /// </summary>
        /// <param name="testString"></param>
        /// <returns>True if the string is only letters, False if not.</returns>
        public static Boolean IsOnlyLettersAndOrDigits(this String testString)
        {


            bool retValue = true;


            try
            {
                // remove white spaces 
                testString = testString.Trim();


                if ((testString.Length == 0))
                {
                    retValue = false;
                }

                // We have a string with no spaces.

                if ((retValue))
                {
                    // Loop through the string, checking each character.
                    for (Int32 index = 0; index <= testString.Length - 1; index++)
                    {
                        if ((char.IsLetterOrDigit(testString[index]) == false))
                        {
                            retValue = false;
                            break;
                        }

                    }

                }

            }
            catch (Exception exUnhandled)
            {
                exUnhandled.Data.Add("testString", testString ?? "NULL");

                throw;

            }

            return retValue;
        }

        /// <summary>
        /// This extension for the String object checks the string to see if it is a valid email format.
        /// it does not check whether it is a valid, working email.
        /// </summary>
        /// <param name="email">The email address to test</param>
        /// <returns>True if formatted correctly, False if not.</returns>
        public static Boolean IsEmailFormat(this String email)
        {

            Boolean retValue = false;

            System.Net.Mail.MailAddress mailAddress = null;


            try
            {
                if (((email != null)))
                {
                    if ((email.Contains("@")))
                    {
                        if ((!email.Contains(" ")))
                        {
                            mailAddress = new System.Net.Mail.MailAddress(email);
                            retValue = true;
                        }
                        else
                        {
                            retValue = false;
                        }
                    }
                    else
                    {
                        retValue = false;
                    }
                }
                else
                {
                    email = "";
                    retValue = true;
                }

            }
            catch (FormatException exFormat)
            {
                // I hate using exceptions as a part of normal flow, but this object
                // does not have a TryParse() option
                retValue = false;

            }
            catch (Exception exUnhandled)
            {
                exUnhandled.Data.Add("email", email ?? "NULL");

                throw;


            }
            finally
            {
                mailAddress = null;
            }

            return retValue;

        }

        /// <summary>
        /// Converts string to date, or returns the default value.
        /// </summary>
        /// <param name="dateString">A string that can be converted to a DateTime variable</param>
        /// <param name="dateDefault">The default value if dateString cannot be converted</param>
        /// <returns>The DateTime value if the string converts, the default value if not</returns>
        public static DateTime GetDateTime(this String dateString, DateTime dateDefault)
        {
            DateTime retVal = dateDefault;

            dateString = dateString ?? "";

            try
            {

                if (dateString.Length > 0)
                {

                    if (!DateTime.TryParse(dateString, out retVal))
                    {
                        retVal = dateDefault;
                    }
                }
            }
            catch (Exception exUnhandled)
            {
                exUnhandled.Data.Add("dateString", dateString ?? "NULL");

                throw;

            }

            return retVal;

        }  // END public static DateTime GetDateTime(this String dateString, DateTime dateDefault)

        /// <summary>
        /// Converts string to decimal value, or returns the default value.
        /// </summary>
        /// <param name="numberString">A string that can be converted to a Decimal variable</param>
        /// <param name="decimalDefault">The default value if numberString cannot be converted</param>
        /// <returns>The Decimal value if the string converts, the default value if not</returns>
        public static Decimal GetDecimal(this String numberString, Decimal decimalDefault)
        {
            Decimal retVal = decimalDefault;

            numberString = numberString ?? "";

            try
            {

                if (numberString.Length > 0)
                {

                    if (!Decimal.TryParse(numberString, out retVal))
                    {
                        retVal = decimalDefault;
                    }
                }
            }
            catch (Exception exUnhandled)
            {
                exUnhandled.Data.Add("numberString", numberString ?? "NULL");

                throw;

            }

            return retVal;

        }  // END public static Decimal GetDecimal(this String numberString, Decimal decimalDefault)

        /// <summary>
        /// Converts string to Int32 value, or returns the default value.
        /// </summary>
        /// <param name="numberString">A string that can be converted to an Int32 variable</param>
        /// <param name="integerDefault">The default value if numberString cannot be converted</param>
        /// <returns>The Int32 value if the string converts, the default value if not</returns>
        public static Int32 GetInt32(this String numberString, Int32 integerDefault)
        {
            Int32 retVal = integerDefault;

            numberString = numberString ?? "";

            try
            {

                if (numberString.Length > 0)
                {

                    if (!Int32.TryParse(numberString, out retVal))
                    {
                        retVal = integerDefault;
                    }
                }
            }
            catch (Exception exUnhandled)
            {
                exUnhandled.Data.Add("numberString", numberString ?? "NULL");

                throw;

            }

            return retVal;

        }  // END public static Int32 GetInt32(this String numberString, Int32 integerDefault)

        /// <summary>
        /// Converts string to Int64 value, or returns the default value.
        /// </summary>
        /// <param name="numberString">A string that can be converted to a Int64 variable</param>
        /// <param name="integerDefault">The default value if numberString cannot be converted</param>
        /// <returns>The Int64 value if the string converts, the default value if not</returns>
        public static Int64 GetInt64(this String numberString, Int64 integerDefault)
        {
            Int64 retVal = integerDefault;

            numberString = numberString ?? "";

            try
            {

                if (numberString.Length > 0)
                {

                    if (!Int64.TryParse(numberString, out retVal))
                    {
                        retVal = integerDefault;
                    }
                }
            }
            catch (Exception exUnhandled)
            {
                exUnhandled.Data.Add("numberString", numberString ?? "NULL");

                throw;

            }

            return retVal;

        }  // END public static Int64 GetInt64(this String numberString, Int64 integerDefault)




    }
}
