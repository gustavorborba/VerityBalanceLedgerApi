namespace BalanceLedgerApi.Util
{
    public class RetryUtilService
    {
        public static async Task<T> RetryOnExceptionAsync<T>(Func<T> func, ILogger logger, int secondsBetweenRetries = 15, int retryCount = 3)
        {
            try
            {
                return await Task.Run(func);
            }
            catch (Exception ex)
            {
                logger.LogInformation(ex, "Error on attempt. Retrying in {secondsBetweenRetries} seconds.", secondsBetweenRetries);

                if (retryCount > 0)
                {
                    retryCount--;
                    await Task.Delay(TimeSpan.FromSeconds(secondsBetweenRetries));
                    return await RetryOnExceptionAsync(func, logger, secondsBetweenRetries, retryCount);
                }
                throw;
            }
        }
    }
}
