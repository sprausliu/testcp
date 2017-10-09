using System;
using System.DirectoryServices;

namespace Common
{
    /// <summary>
    /// Description of ADOperation.
    /// </summary>
    static class ADOperation
    {
        static readonly string domain = "zone1.scb.net";
        public static Staff IsAuthenticated(string username, string pwd)
        {
            Staff staff = null;
            string domainAndUsername = domain + @"\" + username;
            SearchResult result = null;

            DirectoryEntry entry = new DirectoryEntry("LDAP://" + domain, domainAndUsername, pwd);
            try
            {
                Object obj = entry.NativeObject;
                DirectorySearcher search = new DirectorySearcher(entry);
                search.Filter = "(SAMAccountName=" + username + ")";
                // search.PropertiesToLoad.Add("cn");
                result = search.FindOne();
                /*
                System.DirectoryServices.DirectoryEntry MyUser = result.GetDirectoryEntry();
                string OUPath = MyUser.Parent.Path.Substring(21);
                */
                if (null != result)
                {
                    staff = new Staff();
                    staff.BankID = username;
                    staff.OUPath = result.Properties["adspath"][0].ToString();
                    staff.Zone = result.Properties["userprincipalname"][0].ToString();
                    staff.Zone = staff.Zone.Substring(8);
                    staff.DisplayName = result.Properties["displayname"][0].ToString();
                    staff.Email = result.Properties["mail"][0].ToString();
                    if (result.Properties["ipphone"].Count > 0)
                    {
                        staff.Telephone = result.Properties["ipphone"][0].ToString();
                    }
                    else
                    {
                        staff.Telephone = "";
                    }
                    staff.Country = result.Properties["co"][0].ToString();
                    staff.Location = result.Properties["l"][0].ToString();
                    staff.Company = result.Properties["company"][0].ToString();
                    if (result.Properties["department"].Count > 0)
                    {
                        staff.Department = result.Properties["department"][0].ToString();
                    }
                    else
                    {
                        staff.Department = "";
                    }
                    if (result.Properties["manager"].Count > 0)
                    {
                        staff.LineManager = result.Properties["manager"][0].ToString();
                        if (staff.LineManager.ToLower().Contains("cn="))
                        {
                            staff.LineManager = staff.LineManager.Substring(3, 7);
                        }
                        else
                        {
                            staff.LineManager = null;
                        }
                    }

                }
            }
            catch (Exception)
            {
                return staff;
                //                // log exception
                //                throw ex;
            }

            return staff;
        }
    }
}
