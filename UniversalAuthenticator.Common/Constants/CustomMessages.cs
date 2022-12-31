using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversalAuthenticator.Common.Constants
{
    public class CustomMessages
    {
        #region Onboarding Messages

        public const string Email_Required = "Email is required";

        public const string Email_already_exists = "Email address already exist";
        
        public const string FirstName_Required = "First name is required";

        public const string Gender_Required = "Gender is required";

        public const string LastName_Required = "Last name is required";

        public const string InvalidUsername = "Username cannot contains special characters";

        public const string Phone_Required = "Phone number is required";

        public const string Phone_already_exists = "Phone number already exist";

        public const string Username_already_exists = "Username already exist";
        
        public const string Username_Required = "Username is required";             
        
        public const string Username_too_long = "Username cannot exceed 50 characters";

        public const string Role_is_required = "Role(s) is required";

        public const string Invalid_roles_supplied = "Invalid role(s) supplied";

        public const string Invalid_permission_supplied = "Invalid permission(s) supplied";

        #endregion

        #region Login Response Messages
        public const string UserNotFound = "User record does not exist";

        public const string AccountInactive = "Your profile is currently not active, kindly contact administrator.";

        public const string PasswordTrialExceeded = "Password tries exceeded. Please contact administrator for assistance.";

        public const string InvalidLogin = "Invalid Login Details.";

        public const string TemporaryPasswordExpired = "Temporary password has expired, please contact administrator.";

        public const string PromptChangePassword = "Kindly change your temporary password";

        public const string PromptMultifactorAuthentication = "Multi-factor authentication is required";

        public const string InvalidRefreshToken = "Invalid Refresh Token";

        public const string UsernameRequired = "Username is required";

        public const string PasswordRequired = "Password is required";
        #endregion

        #region General Messages

        public const string Successful = "Request successfully completed";

        public const string InvalidEmailFormat = "The format of the email entered is invalid";

        public const string SomethingWentWrong = "Oops! Something went wrong, please try again later";

        public const string EmailTemplateNotFound = "Email Template not found";

        public const string SystemConfigNotFound = "System Configuration not found";

        #endregion

    }
}
