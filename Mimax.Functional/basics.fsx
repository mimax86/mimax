let rnd = System.Random(7)
let test1() = rnd.Next()
test1()

open System.Windows.Forms
let form = new Form()
form.Show()
form.Width <- 400
form.Height <- 400
form.Text <- "Hello from F#!"
System.Threading.Thread.Sleep(2000)
form.Width <- 600
form.Height <- 600

let t = ()
let printAge age =
   System.Console.WriteLine(age.ToString())
let result = printAge(12).GetHashCode()
let a = t = result