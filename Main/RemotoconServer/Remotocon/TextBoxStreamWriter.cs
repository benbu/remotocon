using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Threading;

namespace Remotocon
{
    public class TextBoxStreamWriter : TextWriter
    {
        Predicate<string> StringPredicate;
        Predicate<char> CharPredicate;

        public TextBoxStreamWriter(Predicate<char> charPred, Predicate<string> stringPred)
        {
            this.StringPredicate = stringPred;
            this.CharPredicate = charPred;
        }

        public override void Write(string value)
        {
            lock (this)
            {
                StringPredicate.Invoke(value);
            }
        }

        public override void Write(char value)
        {
            lock (this)
            {
                CharPredicate.Invoke(value);
            }
        }

        public override void WriteLine(string value)
        {
            Write(value + '\n');
        }

        public override Encoding Encoding
        {
            get { return System.Text.Encoding.UTF8; }
        }
    }
}
