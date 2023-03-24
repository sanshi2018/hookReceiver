using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;
namespace MsgRecipe
{
    

    public class MessageReceiver : Form
    {
        public MessageReceiver()
        {
            // 注册接收消息的消息处理函数
            this.AddMessageHandler(0x0201, this.OnMouseDown);
            this.AddMessageHandler(0x0202, this.OnMouseUp);
            // 处理键盘消息
            this.AddMessageHandler(0x0100, this.OnKeyDown);
            this.AddMessageHandler(0x0101, this.OnKeyUp);
            // 设置窗口title
            this.Text = "消息接收窗口";
        }

        private void OnKeyUp(Message obj)
        {
            // 打印那个键盘被按下
            Debug.WriteLine($"键盘:{obj.WParam}被弹起");
            MessageBox.Show($"键盘:{obj.WParam}被弹起");
        }

        private void OnKeyDown(Message obj)
        {
            Debug.WriteLine($"键盘:{obj.WParam}被按下");
            MessageBox.Show($"键盘:{obj.WParam}被按下");
        }

        protected override void WndProc(ref Message m)
        {
            // 调用所有注册的消息处理函数
            foreach (var handler in _messageHandlers)
            {
                if (handler.Key == m.Msg)
                {
                    handler.Value(m);
                    return;
                }
            }

            base.WndProc(ref m);
        }

        // 添加一个消息处理函数
        private void AddMessageHandler(int msg, Action<Message> handler)
        {
            _messageHandlers.Add(msg, handler);
        }

        // 鼠标按下消息处理函数
        private void OnMouseDown(Message m)
        {
            int x = (int)m.LParam & 0xFFFF;
            int y = ((int)m.LParam >> 16) & 0xFFFF;
            Debug.WriteLine($"Mouse down at ({x}, {y})");
            MessageBox.Show($"Mouse down at ({x}, {y})");
        }

        // 鼠标松开消息处理函数
        private void OnMouseUp(Message m)
        {
            int x = (int)m.LParam & 0xFFFF;
            int y = ((int)m.LParam >> 16) & 0xFFFF;
            Debug.WriteLine($"Mouse up at ({x}, {y})");
            MessageBox.Show($"Mouse up at ({x}, {y})");
        }

        private readonly Dictionary<int, Action<Message>> _messageHandlers = new Dictionary<int, Action<Message>>();
    }

}