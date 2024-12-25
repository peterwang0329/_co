using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace final
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        Dictionary<string, string> cccccc = new Dictionary<string, string>()
        {
            {"0",  "101010"},
            {"1",  "111111"},
            {"-1", "111010"},
            {"D",  "001100"},
            {"A",  "110000"},
            {"!D", "001101"},
            {"!A", "110001"},
            {"-D", "001111"},
            {"-A", "110011"},
            {"D+1","011111"},
            {"1+D","011111"},
            {"A+1","110111"},
            {"1+A","110111"},
            {"D-1","001110"},
            {"A-1","110010"},
            {"D+A","000010"},
            {"A+D","000010"},
            {"D-A","010011"},
            {"A-D","000111"},
            {"D&A","000000"},
            {"A&D","000000"},
            {"D|A","010101"},
            {"A|D","010101"},
            {"M",  "110000"},
            {"!M", "110001"},
            {"-M", "110011"},
            {"M+1","110111"},
            {"1+M","110111"},
            {"M-1","110010"},
            {"D+M","000010"},
            {"M+D","000010"},
            {"D-M","010011"},
            {"M-D","000111"},
            {"D&M","000000"},
            {"M&D","000000"},
            {"D|M","010101"},
            {"M|D","010101"}
        };

        Dictionary<string, string> ddd = new Dictionary<string, string>()
        {
            {"null","000"},
            {"M",   "001"},
            {"D",   "010"},
            {"MD",  "011"},
            {"A",   "100"},
            {"AM",  "101"},
            {"AD",  "110"},
            {"AMD", "111"}
        };

        Dictionary<string, string> jjj = new Dictionary<string, string>()
        {
            {"null","000"},
            {"JGT", "001"},
            {"JEQ", "010"},
            {"JGE", "011"},
            {"JLT", "100"},
            {"JNE", "101"},
            {"JLE", "110"},
            {"JMP", "111"}
        };

        private int count = 16;

        Dictionary<string, int> symbolTable = new Dictionary<string, int>()
        {
            {"R0", 0}, 
            {"R1", 1}, 
            {"R2", 2}, 
            {"R3", 3},
            {"R4", 4}, 
            {"R5", 5}, 
            {"R6", 6}, 
            {"R7", 7},
            {"R8", 8}, 
            {"R9", 9}, 
            {"R10", 10}, 
            {"R11", 11},
            {"R12", 12}, 
            {"R13", 13}, 
            {"R14", 14}, 
            {"R15", 15},
            {"SCREEN", 16384}, 
            {"KBD", 24576},
            {"SP", 0}, 
            {"LCL", 1}, 
            {"ARG", 2}, 
            {"THIS", 3}, 
            {"THAT", 4}
        };

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string origin_str = text.Text;
            List<string> turn_str = new List<string>();
            List<string> ans_str = new List<string>(); //XXXaccccccdddjjj
            bool error = false;
            while (origin_str.Contains("\n"))
            {
                string set = origin_str.Substring(0, origin_str.IndexOf("\n")-1);
                if(set.Contains("//"))
                    set = set.Substring(0, origin_str.IndexOf("//"));
                set = set.Replace(" ", "");
                origin_str = origin_str.Remove(0,origin_str.IndexOf("\n")+1);
                if (set == "")
                    continue;
                if (set.Contains("(") & set.Contains(")"))
                {
                    string RAM = set.Replace("(", "");
                    RAM = RAM.Replace(")", "");
                    if(!symbolTable.ContainsKey(RAM))
                        symbolTable.Add(RAM,turn_str.Count);
                    continue;
                }
                turn_str.Add(set);
            }
            if (origin_str.Length > 0)
            {
                if (origin_str.Contains("//"))
                    origin_str = origin_str.Substring(0, origin_str.IndexOf("//"));
                origin_str = origin_str.Replace(" ", "");
                turn_str.Add(origin_str);
            }

            for(int i = 0; i < turn_str.Count; i++)
            {
                string turn = turn_str[i];
                if (string.IsNullOrEmpty(turn) || (turn.Contains("(") & turn.Contains(")")))
                    continue;
                else
                {
                    if (turn.Contains("@"))
                        turn = binary_addr(turn);
                    else if (turn.Contains(";"))
                        turn = j(turn);
                    else if (turn.Contains("="))
                        turn = Calculator(turn);

                    if (turn == "error")
                       error = true;
                    ans_str.Add(turn);
                }
            }
            ans.Text = error ? "Error!" : string.Join("\n", ans_str);
        }

        private string binary_addr(string turn)
        {
            string binary = turn.Substring(1);  //清除@
            if (int.TryParse(binary, out int addr))
                return Convert.ToString(addr, 2).PadLeft(16, '0');
            else
            {
                if (!symbolTable.ContainsKey(binary))
                {
                    symbolTable.Add(binary, count);
                    count++;
                }
                addr = symbolTable[binary];
                return Convert.ToString(addr, 2).PadLeft(16, '0'); ;
            }
        }
        private string j(string turn)
        {
            string[] parts = turn.Split(';');
            if (parts.Length != 2)
                return "error";

            string c = parts[0];
            string j = parts[1];

            string a = c.Contains("M") ? "1" : "0";

            string c_Binary = cccccc.ContainsKey(c) ? cccccc[c] : "error";
            string j_Binary = jjj.ContainsKey(j) ? jjj[j] : "error";

            if (c_Binary == "error" || j_Binary == "error")
                return "error";

            return "111" + a + c_Binary + "000" + j_Binary;
        }
        private string Calculator(string turn) //M、A、D
        {
            string[] parts = turn.Split('=');
            if (parts.Length != 2)
                return "error";

            string d = parts[0];
            string c = parts[1];

            string a = c.Contains("M") ? "1" : "0";

            string d_Binary = ddd.ContainsKey(d) ? ddd[d] : "error";
            string c_Binary = cccccc.ContainsKey(c) ? cccccc[c] : "error";

            if (d_Binary == "error" || c_Binary == "error")
                return "error";

            return "111" + a + c_Binary + d_Binary + "000";
        }
    }
}