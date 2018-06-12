using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using System.Web.Http.ModelBinding;

namespace Sensoria.SmokeTest.Api.Models
{
    public enum SensoriaApiErrorCodes : long
    {
        NONE = 0xF0000 - 1,
        GENERAL_FAILURE = 0xF0000,
        INTERNAL_ERROR = 0xF0001,
        USER_ALREADY_EXISTS = 0xF0002,
        USER_NOT_FOUND = 0xF0003,
        USER_ACTIVATION_CODE_INVALID = 0xF0004,
        USER_DOES_NOT_NEED_ACTIVATION = 0xF0005,
        ACCESS_TO_USER_DENIED = 0xF0006,
        USER_NOT_AUTHORIZED = 0xF0007,
        FOOT_PROFILE_NOT_FOUND = 0xF0008,
        FOOT_PROFILE_EXISTING_FOR_USER = 0xF0009,
        MODEL_STATE_INVALID = 0xF000A,
        CLOSET_ITEMS_NOT_FOUND = 0xF000B,
        CLOSET_ITEM_EXISTING_FOR_USER = 0xF000C,
        CLOSET_ITEM_NOT_EXIST = 0xF000D,
        ACTIVATION_EMAIL_SEND_FAILED = 0xF000F,
        USER_CREATED_EMAIL_FAILED = 0xF0010,
        SESSION_NOT_FOUND = 0xF0011,
        APP_PREVIEW_CONTACT_EXISTING_FOR_USER = 0xF0012,
        APP_PREVIEW_CONTACT_NOT_FOUND = 0xF0013,
        WELCOME_EMAIL_SEND_FAILED = 0xF0014,
        SESSION_IS_NOT_VALID = 0xF0015,
        DATA_FORMAT_NOT_RECOGNIZED = 0xF0016,
        WORKSPACE_NOT_FOUND = 0xF0017,
        INPUT_MISMATCH = 0xF0018,
        SESSION_WITHOUT_SPLITS = 0xF0019,
        SPLIT_WITHOUT_SEGMENTS = 0xF001A,
        START_OR_END_IS_NULL = 0xF001B,
        ACTIVITY_TOO_LONG = 0xF001C,
        SESSION_WITHOUT_RECORDED_DATETIME = 0xF001D,
        FEEDBACK_NOT_FOUND = 0xF001E,
        MALFORMED_JSON = 0xF001F,
        FIRMWARE_NOT_FOUND = 0xF0020,
        FOOT_PALS_NOT_FOUND = 0xF0021,
        TRANSLATION_NOT_FOUND = 0xF0022,
        USER_SETTINGS_NOT_FOUND = 0xF0023,
        VIRTUAL_COACHES_NOT_FOUND = 0xF0024,
        TRAININGPLAN_NOT_FOUND = 0xF0025,
        WORKOUTS_NOT_FOUND = 0xF0026,
        USER_NOT_ON_TRAININGPLAN = 0xF0027,
        USER_ALREADY_ON_TRAININGPLAN = 0xF0028,
        TERMS_OF_USE_NOT_FOUND = 0xF0029,
        VALUE_IS_INCORRECT = 0xF0030,
        CLOSETITEMID_MUST_BE_INTEGER = 0xF0031,
        TRAINING_PLAN_STARTDATE_CANNOT_BE_IN_THE_PAST = 0xF0032,
        FOOT_PAL_NOT_FOUND = 0xF0033,
        TERMS_OF_USE_ALREADY_ACCEPTED = 0xF0034,
        PRODUCT_NOT_FOUND = 0xF0035,
        ACHIEVEMENTS_NOT_FOUND = 0xF0036,
        TRAINING_PLAN_ALREADY_REMOVED = 0xF0037,
        NO_DATATIPS_FOUND = 0xF0038,
        PROBLEM_FETCHING_DATATIPS_FOR_SESSION = 0xF0039
    }

    public class SensoriaApiResult<T>
    {
        /// <summary>
        /// The 
        /// </summary>
        public HttpStatusCode StatusCode;
        public T APIResult;
        public HttpError HttpError;
        public Exception Exception;
        public ModelStateDictionary ModelState;

        protected const string SensoriaApiErrorCodeKey = "SensoriaApiErrorCode";

        public static Dictionary<SensoriaApiErrorCodes, string> ErrorMessages = new Dictionary<SensoriaApiErrorCodes, string>()
        {
            { SensoriaApiErrorCodes.NONE,                                   "Not a failure"},
            { SensoriaApiErrorCodes.GENERAL_FAILURE,                        "Sensoria API unknown failure"},
            { SensoriaApiErrorCodes.INTERNAL_ERROR,                         "Sensoria API internal error"},
            { SensoriaApiErrorCodes.USER_ALREADY_EXISTS,                    "User already exists"},
            { SensoriaApiErrorCodes.USER_NOT_FOUND,                         "User not found"},
            { SensoriaApiErrorCodes.USER_ACTIVATION_CODE_INVALID,           "Activation code is invalid"},
            { SensoriaApiErrorCodes.USER_DOES_NOT_NEED_ACTIVATION,          "User already activated"},
            { SensoriaApiErrorCodes.ACCESS_TO_USER_DENIED,                  "Access to user denied"},
            { SensoriaApiErrorCodes.USER_NOT_AUTHORIZED,                    "Client is not authorized for this call"},
            { SensoriaApiErrorCodes.FOOT_PROFILE_NOT_FOUND,                 "This foot profile does not exist"},
            { SensoriaApiErrorCodes.FOOT_PROFILE_EXISTING_FOR_USER,         "User already has a foot profile"},
            { SensoriaApiErrorCodes.MODEL_STATE_INVALID,                    "The request is invalid"},
            { SensoriaApiErrorCodes.SESSION_NOT_FOUND,                      "Sessions were not found for this user"},
            { SensoriaApiErrorCodes.APP_PREVIEW_CONTACT_EXISTING_FOR_USER,  "Preview Signup already present for this user"},
            { SensoriaApiErrorCodes.APP_PREVIEW_CONTACT_NOT_FOUND,          "Preview Signup not found"},
            { SensoriaApiErrorCodes.WELCOME_EMAIL_SEND_FAILED,              "Failed to send welcome email to the user"},
            { SensoriaApiErrorCodes.DATA_FORMAT_NOT_RECOGNIZED,             "The data format requested is not recognized"},
            { SensoriaApiErrorCodes.WORKSPACE_NOT_FOUND,                    "The requested workspace was not found"},
            { SensoriaApiErrorCodes.SESSION_WITHOUT_SPLITS,                 "Every session must have at least one split" },
            { SensoriaApiErrorCodes.SPLIT_WITHOUT_SEGMENTS,                 "Every split must have at least one segment" },
            { SensoriaApiErrorCodes.START_OR_END_IS_NULL,                   "Start and end times cannot be null" },
            { SensoriaApiErrorCodes.ACTIVITY_TOO_LONG,                      "The activity has exceeded the maximum allowable length"},
            { SensoriaApiErrorCodes.SESSION_WITHOUT_RECORDED_DATETIME,      "A Session requires a recorded datetime to be specified to be valid" },
            { SensoriaApiErrorCodes.FEEDBACK_NOT_FOUND,                     "The requested feedback was not found"},
            { SensoriaApiErrorCodes.FIRMWARE_NOT_FOUND,                     "The firmware image was not found"},
            { SensoriaApiErrorCodes.CLOSET_ITEMS_NOT_FOUND,                 "The closet item was not found"},
            { SensoriaApiErrorCodes.CLOSET_ITEM_EXISTING_FOR_USER,          "The closet item is already existing"},
            { SensoriaApiErrorCodes.CLOSET_ITEM_NOT_EXIST,                  "The closet item does not exist"},
            { SensoriaApiErrorCodes.FOOT_PALS_NOT_FOUND,                     "Footpals not found for user"},
            { SensoriaApiErrorCodes.TRANSLATION_NOT_FOUND,                  "Translations not found"},
            { SensoriaApiErrorCodes.USER_SETTINGS_NOT_FOUND,                "UserSettings not found"},
            { SensoriaApiErrorCodes.VIRTUAL_COACHES_NOT_FOUND,              "Virtual Coaches not found"},
            { SensoriaApiErrorCodes.TRAININGPLAN_NOT_FOUND,                 "A training plan was not found"},
            { SensoriaApiErrorCodes.WORKOUTS_NOT_FOUND,                     "No workouts were found"},
            { SensoriaApiErrorCodes.USER_NOT_ON_TRAININGPLAN,               "User is not on a training plan"},
            { SensoriaApiErrorCodes.USER_ALREADY_ON_TRAININGPLAN,           "User is already on a training plan"},
            { SensoriaApiErrorCodes.TERMS_OF_USE_NOT_FOUND,                 "The requested Terms of Use does not exist" },
            { SensoriaApiErrorCodes.VALUE_IS_INCORRECT,                     "Input value entered is not valid"},
            { SensoriaApiErrorCodes.CLOSETITEMID_MUST_BE_INTEGER,           "If ClosetItemId is supplied, it must be an integer" },
            { SensoriaApiErrorCodes.TRAINING_PLAN_STARTDATE_CANNOT_BE_IN_THE_PAST,    "The start date cannot be in the past." },
            { SensoriaApiErrorCodes.FOOT_PAL_NOT_FOUND,                     "Footpal not found"},
            { SensoriaApiErrorCodes.TERMS_OF_USE_ALREADY_ACCEPTED,          "The user has already accepted the current Terms of Use." },
            { SensoriaApiErrorCodes.PRODUCT_NOT_FOUND,                      "The specified product does not exist." },
            { SensoriaApiErrorCodes.ACHIEVEMENTS_NOT_FOUND,                 "User did not achieve any badges or PRs." },
            { SensoriaApiErrorCodes.TRAINING_PLAN_ALREADY_REMOVED,          "The training plan has been already removed from the user profile." },
            { SensoriaApiErrorCodes.NO_DATATIPS_FOUND,                      "No data tips were found for this user." },
            { SensoriaApiErrorCodes.PROBLEM_FETCHING_DATATIPS_FOR_SESSION,  "An unknown problem occured while fetching data tips for this session." }
        };

        public bool IsSuccess
        {
            get
            {
                int statusCodeInt = (int)StatusCode;
                return isSuccess(statusCodeInt);
            }
        }

        public SensoriaApiErrorCodes SensoriaError
        {
            get
            {
                if (HttpError == null)
                    return SensoriaApiErrorCodes.NONE;

                if (HttpError.ContainsKey(SensoriaApiErrorCodeKey))
                {
                    return (SensoriaApiErrorCodes)HttpError[SensoriaApiErrorCodeKey];
                }
                else
                { 
                    return SensoriaApiErrorCodes.GENERAL_FAILURE;
                }
            }
        }

        public string ErrorMessage(bool includeExceptionMessage = false, bool includeExceptionDetail = false)
        {
            string formatMessage = "Sensoria Error: {0} \nHttp Code: {1}";

            if (includeExceptionMessage)
                formatMessage += "\nException Error: {2}";

            if (includeExceptionDetail)
                formatMessage += "\nException Detail: {3}";

            return string.Format(formatMessage, this.StatusCode.ToString(), (HttpError == null) ? "" : HttpError.Message, (Exception == null) ? "" : Exception.Message, (Exception == null) ? "" : Exception.ToString());
        }

        private static bool isSuccess(int statusCode)
        {
            return (statusCode >= 200 && statusCode < 300);
        }

    }
}
