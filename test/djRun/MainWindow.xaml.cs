using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Text.RegularExpressions;
using WpfApplicationHotKey.WinApi;


namespace djRun
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        private HotKey _hotkey;
        private Dictionary<string, string> dic = new Dictionary<string, string>();
        private int cmd_len = 0;

        public MainWindow()
        {
            InitializeComponent();

            Loaded += (s, e) =>
            {
                _hotkey = new HotKey(ModifierKeys.Alt, Keys.OemSemicolon, this);
                _hotkey.HotKeyPressed += (k) => hotkey_action();
            };


            dic.Clear();

            dic.Add("/Quit", "");

            String index = System.Environment.CurrentDirectory + '\\' + "index.txt";
            string[] lines = System.IO.File.ReadAllLines(index);

            for (int i = 0; i < lines.Length; i++)
            {
                string[] fa = lines[i].Split('|');

                if (fa.Length > 1){
                    try{
                        dic.Add(fa[0], fa[1]);
                    }catch{

                    }
                    
                }
            }

            main_text.Focus();
        }

        private void Main_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape) {
                main_text.Text = "";
                this.Hide();
            }

            if (e.Key == Key.Enter)
            {
                if (sender == main_text) {
                    //MessageBox.Show(main_list.Items[0].ToString());
                    cmd_execute(main_list.Items[0].ToString());
                }
                else if (sender == main_list) {
                    //MessageBox.Show(main_list.SelectedItem.ToString());
                    cmd_execute(main_list.SelectedItem.ToString());
                }
            }

            if (sender == main_text) {
                if (e.Key == Key.Down)
                {
                    //main_list.SelectedIndex = 0;
                    main_list.Focus();
                }
            }
        }

        private void main_text_TextChanged(object sender, TextChangedEventArgs e)
        {

            if (main_text.Text.Length == 0){
                main_list.Items.Clear();
                return;
            }

            Regex rgx = new Regex(make_reg_str(main_text.Text), RegexOptions.IgnoreCase);

            if (main_text.Text.Length != 1 && cmd_len < main_text.Text.Length){
                for (int ri = main_list.Items.Count-1; ri >= 0; ri--){
                    if (rgx.Matches(main_list.Items[ri].ToString()).Count == 0){
                        main_list.Items.RemoveAt(ri);
                    }
                }
            }
            else
            {
                main_list.Items.Clear();
                Dictionary<string, string>.KeyCollection keys = dic.Keys;

                foreach(String k in keys) {
                    if (rgx.Matches(k).Count > 0){
                        main_list.Items.Add(k);
                    }
                }
            }

            cmd_len = main_text.Text.Length;
        }

        String make_reg_str(String s) {
            StringBuilder sb = new StringBuilder();
            String al = ".*";

            sb.Append(al);

            for (int i = 0 ; i < s.Count(); i++) {
                sb.Append(s[i]);
                sb.Append(al);
            }

            return sb.ToString();
        }

        private void hotkey_action() {
            this.Show();
            main_text.Focus();
        }

        private void cmd_execute(String key){
            if (key.Substring(0, 1) == "/")
            {
                if (key == "/Quit")
                {
                    this.Close();
                }
            }
            else {
                System.Diagnostics.Process.Start(dic[key]);
            }
            main_text.Text = "";
            this.Hide();
        }

    }
}
