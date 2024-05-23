using System.ComponentModel.DataAnnotations;

namespace MedPedia.Common.Utilities.Enums;

public enum Messages
{
    [Display(Name = nameof(Unauthorized), Description = "خطای احراز هویت")]
    Unauthorized = 401,

    [Display(Name = nameof(NoAccess), Description = "مجاز به دریافت اطلاعات دیگری نیستید.")]
    NoAccess = 430,
    
    [Display(Name = nameof(WrongValidationType), Description = "")]
    WrongValidationType = 450,

    [Display(Name = nameof(NotFound), Description = "موردی یافت نشد")]
    NotFound = 404,

    [Display(Name = nameof(UserNotFound), Description = "کاربر یافت نشد")]
    UserNotFound = 405,


    [Display(Name = nameof(UsernameExists), Description = "نام کاربری نباید تکراری باشد.")]
    UsernameExists = 963,
    
    [Display(Name = nameof(UsernameNotExists), Description = "نام کاربری قابل قبول می باشد.")]
    UsernameNotExists = 963,
    
    [Display(Name = nameof(ServerError), Description = "خطای سرور")]
    ServerError = 500,

    [Display(Name = nameof(SessionNotFound), Description = "نشست یافت نشد.")]
    SessionNotFound = 442,

    [Display(Name = nameof(UsernameOrPasswordIncorrect), Description = "نام کاربری یا کلمه عبور نادرست است.")]
    UsernameOrPasswordIncorrect = 440,
    
    [Display(Name = nameof(AuthCodeWrong), Description = "کد فعال سازی اشتباه وارد شده است.")]
    AuthCodeWrong = 1246,
    
    
    [Display(Name = nameof(WaitUntilSmsSend), Description = "لطفا تا زمان ارسال پیامک صبر کنید.")]
    WaitUntilSmsSend = 110,
    
    [Display(Name = nameof(SuccessFully), Description = "با موفقیت ذخیره شد.")]
    SuccessFully = 1245,

}