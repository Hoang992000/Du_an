namespace DemoVnPay.Helper
{
    public class RetunObject<T>
    {
        public T data { get; set; }
        public string mess { get; set; }
        public int statusCode { get; set; }
        public bool errorOccurred
        {
            get
            {
                if (statusCode >= 400)
                {
                    return true;
                }
                return false;

            }
            private set { }
        }
    }
}
