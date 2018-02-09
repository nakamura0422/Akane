using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Akane
{
    class Program
    {
        /// <summary>
        /// Application Entry
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            //Inflameを生成する関数
            Func<string,string> generate = gen =>
            {
                return $"{DateTime.Now.ToString("HH:mm:ss")} : {gen}、{new string[] { "A", "B", "C" }.OrderBy(x => Guid.NewGuid()).First()}";
            };

            //名前を受け取る
            Console.WriteLine("名前plz");
            var name = Console.ReadLine();

            //タスクを作成して発火
            var task = Observable.Repeat(name, 9)
                .Zip(Observable.Interval(TimeSpan.FromMilliseconds(1000)), (x, _) => x)
                .Subscribe(x => Console.WriteLine(generate(x)), () => Console.WriteLine("------終了------"));

            //終了待機
            Console.ReadLine();

            //使い終わったスレッドは削除しとく
            task.Dispose();
        }
    }
}
