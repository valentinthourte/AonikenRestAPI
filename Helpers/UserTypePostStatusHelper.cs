using AonikenRestAPI.Enums;

namespace AonikenRestAPI.Helpers
{
    public class UserTypePostStatusHelper
    {
        public static bool userCanGetPostsByStatus(PostStatusEnum postStatus, UserTypesEnum userType)
        {
            bool result = false;
            switch (postStatus)
            {
                case PostStatusEnum.rejected:
                {
                    if (userType == UserTypesEnum.writer)
                    {
                        result = true;
                    }
                    break;
                }
                case PostStatusEnum.pending:
                {
                    if (userType == UserTypesEnum.editor)
                    {
                        result = true;
                    }
                    break;
                }
                case PostStatusEnum.approved:
                {

                    break;
                }
                case PostStatusEnum.any:
                {
                    break;
                }
            }
            return result;
        }
    }


}
