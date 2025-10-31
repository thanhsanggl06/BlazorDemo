namespace BlazorSolution.MsgBox
{
    public class MessageBoxModel
    {
        public string Title { get; set; } = "通知"; // Thông báo
        public string Message { get; set; } = "";
        public MessageBoxType Type { get; set; } = MessageBoxType.Info;
        public MessageBoxButtons Buttons { get; set; } = MessageBoxButtons.OK;
        public string OkButtonText { get; set; } = "OK";
        public string CancelButtonText { get; set; } = "キャンセル"; // Hủy
        public string YesButtonText { get; set; } = "はい"; // Có
        public string NoButtonText { get; set; } = "いいえ"; // Không
        public string DefaultValue { get; set; } = "";
        
        /// <summary>
        /// Hiển thị backdrop/background cho modal (mặc định: true)
        /// </summary>
        public bool ShowBackdrop { get; set; } = true;
        
        /// <summary>
        /// Cho phép đóng modal khi click backdrop (mặc định: true)
        /// </summary>
        public bool CloseOnBackdropClick { get; set; } = true;
        
        /// <summary>
        /// Hiển thị icon trong body của modal (mặc định: true)
        /// </summary>
        public bool ShowBodyIcon { get; set; } = true;
        
        /// <summary>
        /// Kích thước font cho message body: "small", "normal", "large" (mặc định: "normal")
        /// </summary>
        public string MessageFontSize { get; set; } = "normal";
    }

    public enum MessageBoxType
    {
        Info,
        Success,
        Warning,
        Error,
        Question,
        Prompt
    }

    public enum MessageBoxButtons
    {
        OK,
        OKCancel,
        YesNo,
        YesNoCancel
    }

    public enum MessageBoxResult
    {
        None,
        OK,
        Cancel,
        Yes,
        No
    }
}
