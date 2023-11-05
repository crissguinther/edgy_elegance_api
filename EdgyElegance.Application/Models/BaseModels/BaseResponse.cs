namespace EdgyElegance.Application.Models.BaseModels {
    public abstract class BaseResponse {
        /// <summary>
        /// Indicates if the request was successful
        /// </summary>
        public bool Success { get; set; } = false;

        /// <summary>
        /// The <see cref="List{T}"/> of errors that ocurred
        /// </summary>
        public List<string>? Errors { get; set; } = new List<string>();
    }
}
