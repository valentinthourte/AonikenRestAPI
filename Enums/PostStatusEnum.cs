namespace AonikenRestAPI.Enums
{
    public enum PostStatusEnum 
    {
        // Idealmente el enum contendría chars, ya que son mas entendibles al momento de almacenarlos
        // Sin embargo, la tarea de verificar si un valor pertenece al enum (PostValidations::validatestatusForGetPosts)se complejiza mucho al utilizar el char.
        // Por eso, utilizo ints.

        pending = 1,
        approved = 2,
        rejected = 3,
        any = 4
    }
}
