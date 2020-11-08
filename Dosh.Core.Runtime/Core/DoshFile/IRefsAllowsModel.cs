namespace Dosh.Core.DoshFile
{
    /// <summary>
    /// Reference allowed interface
    /// </summary>
    public interface IRefsAllowsModel
    {
        /// <summary>
        /// Reference resolution.
        /// </summary>
        /// <param name="definition">definition model</param>
        void RefsResolution(Definition definition);
    }
}
