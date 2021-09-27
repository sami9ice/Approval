namespace Application.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBarcodeGeneratorService
    {
        /// <summary>
        /// Generates the bar code.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <returns></returns>
        string GenerateBarCode(string code);
    }
}