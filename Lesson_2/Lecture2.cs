using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace App
{
    class Program
    {
        static void Main(string[] args)
        {
    #region none
            Husky AHusky = new Husky("hah", "#000");
            AHusky.OnStartBark += AHusky_OnStartBark;
            AHusky.OnEndBark += AHusky_OnEndBark;
            AHusky.Bark();

            var temp = new WebLengthClass();
            Console.ReadKey();
#endregion
        }
    #region 事件
        private static void AHusky_OnStartBark(object sender, EventArgs e)
        {
            var husky = sender as Husky;
            Console.WriteLine($"{husky.Name} will bark.");
        }

        private static void AHusky_OnEndBark(object sender, EventArgs e)
        {
            var husky = sender as Husky;
            Console.WriteLine($"{husky.Name} stops barking.");
        }
#endregion
    }

    #region 属性访问器
    /// <summary>
    /// get方法: 获取属性的内容
    /// set方法: 设置属性的内容
    /// get, set方法的访问修饰符:
    ///          这两个方法的访问修饰符有三个:public, protected, private
    ///          在默认情况下它们的访问修饰符是public
    ///          三个访问修饰符的区别:
    ///          public: 在类外可以被访问到
    ///          protected: 在此类以及继承此类的子类可以被访问到
    ///          private: 只能在此类中被访问到
    ///使用访问修饰符可以很好的设计数据隐藏，提高代码的稳定性
    ///使用get,set标识属性可以让数据状态更清楚，提高代码的可读性和编程效率
    /// </summary>
    public class Timer
    {
        //第一种书写方法
        private double seconds;
        public double Seconds
        {
            get { return seconds; }
            private set { seconds = value; }
        }
        public double Hours
        {
            get { return seconds / 3600; }
            private set { seconds = value * 3600; }
        }
        //第二种书写方法
        public int Second { get; private set; }
    }
    #endregion
    #region 类及其结构简单介绍
    /// <summary>
    /// 当使用类似 Animal temp = new Animal(); 这句来实例化一个对象的时候，
    /// 第89行的Animal()就会被默认调用，而当Animal temp = new Animal("ss");，用这句
    /// 创建一个实例化对象的时候, Animal(string name)就会被调用，
    /// 构造函数是和类拥有相同签名的一个函数， 该函数相当于是入口，当创建一份它的实例化对象的时候，
    /// 这个实例化对象必须从这里进入，从析构函数出去，可以看作这样的事件：当被创建：调用构造函数，当被销毁：调用析构函数
    /// 但是如果你并没有创建构造函数和析构函数的时候，编译器会默认给你创建一个和第89行和97行一样的函数，并进行调用
    /// 类除了有构造函数和析构函数之外还有属性和方法，所谓属性我们已经在上面大致提到过了，属性相当于是被抽象的数据，
    /// 而方法其实就是对函数的重新命名，方法就是我能进行什么操作，比如下面的SetName(string name)就是一个方法,它能进行的操
    /// 作就是当name存在并且不为空的时候给属性Name赋值
    /// 而继承就是从一个类衍生出另外一个类，比如下面的从Animal中衍生出Dog类， 在Dog类中我们只看到了Color属性和Dog构造函数
    /// 但其实每一个Dog都由Color属性和Name属性，同时拥有SetName方法，当一个狗被创建的时候调用狗的构造函数，那Animal的构造函数
    /// 会有什么反应？毕竟也相当于它在某个子集被创建了，实际上当狗的构造函数被调用之后，Animal的构造函数同样会被调用，默认调用的是
    /// Animal()，但我们可以通过修改在第101行的base括号里面的参数来限定那个构造函数被调用，析构函数调用方式恰好相反，先调用Animal的
    /// 后调用Dog的
    /// </summary>
    public class Animal
    {
        public string Name { get; protected set; }
        public Animal() { }
        public Animal(string name) => Name = name;
        public void SetName(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new Exception("name is not define");
            Name = name;
        }
         ~Animal() { }
    }
    public class Dog : Animal 
    {
        public string Color { get; private set; }
        public Dog(string name, string color) : base(name) => Color = color;
    }
    public class Sheep : Animal
    {
        public Sheep() { } 
    }
    #endregion
    #region 事件
    /// <summary>
    /// 下面定义了两个事件，OnStartBark, OnEndBark
    /// 事件可以理解为当一个对象发送出一个信号的时候另一个对象
    /// 接受到信号, 并做出一些相应的回复，在C#里面发射信号是使用的
    /// 是Invoke函数，而信号的中转是依靠EventHandler进行的，EventHandler
    /// 相当是一个被委托的类，当事件被Invoke的时候，事件对应的所绑定的函数就会被触发，
    /// 函数绑定请看第14，15行，使用+=符号绑定第23，29行的函数
    /// 整体流程相当于：在定义事件信号变量的时候同时绑定一个被委托者，同时在被实例化的对象上
    /// 绑定事件处理函数在事件变量被触发的时候，被委托者执行该信号变量绑定的函数
    /// </summary>
    public class Husky : Dog
    {
        public Husky(string name, string color) : base(name, color) { }
        public event EventHandler OnStartBark;
        public event EventHandler OnEndBark;
        public void Bark(UInt32 times = 1, string time = "12:00")
        {
            OnStartBark?.Invoke(this, EventArgs.Empty); 
            Thread.Sleep(1000); 
            var i = (times == 1 ? "time" : "times");
            Console.WriteLine($"{Name} barked {times} {i} on {time}");
            Thread.Sleep(1000);
            OnEndBark?.Invoke(this, EventArgs.Empty);
        }
    }
    #endregion
    #region Async and Await 
    /// <summary>
    /// 异步和等待是CSharp中比较重要的语法之一，下面的流程大致体现了简单的异步过程。
    /// 当我们调用getWebLength()的时候，先输出1，之后进入函数AccessTheWebAsync()
    /// 输出2，之后请求百度页面的Ascii，在请求的时候有一定的延迟，这时候GetStringAsync()
    /// 内部也会执行到await等待任务，当GetStringAsync()内部执行到await的时候，GetStringAsync()
    /// 的任务会继续执行，但同时AccessTheWebAsync()也开始继续执行直到到了await等待GetStringAsync()
    /// 任务的完成才会继续等待，之后它的任务完成后相当于第153行的认为得到了返回结果，getWebLength()继续执行。
    /// 异步的好处是可以同时执行互不干扰的事情，同时在有干扰之前可以暂停等待结果的返回.
    /// </summary>
    public class WebLengthClass { 
        public WebLengthClass() => getWebLength();
        private async void getWebLength(){ 

            Console.WriteLine("1");
            Task<int> getLengthTask = AccessTheWebAsync();

            Console.WriteLine("4");
            int contentLength = await getLengthTask;

            var Text = String.Format("\r\nLength of the downloaded string: {0}.\r\n", contentLength);
            Console.WriteLine(Text);
        }
        private async Task<int> AccessTheWebAsync() {
            try
            {
                Console.WriteLine("2");
                Task<string> getStringTask = new HttpClient().GetStringAsync("https://www.baidu.com");
                Console.WriteLine("3");
                string urlContents = await getStringTask;
                Console.WriteLine("5");
                return urlContents.Length;
            }
            catch (Exception e) 
            {
                Console.WriteLine(e.Message); 
            }
            Console.WriteLine("5");
            return 0;
        }
    }
#endregion
}
