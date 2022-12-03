namespace Flog.Core

type Page<'T,'I,'O> =     
        {
            content: System.IO.Stream
            meta: 'T
            target: 'O option
            source: 'I option
        }

module Page = 
   open System.IO
   open System.Threading.Tasks
   let render (path : 'O -> string) (page: Page<'T,'I,'O>) =
     match page.target with
     | Some target -> 
        task {
            use file = File.Create(path target)
            do page.content.Seek(0,SeekOrigin.Begin) |> ignore
            do! page.content.CopyToAsync(file)
        } :> Task    
     | None -> Task.CompletedTask
   let withSource source page =
       { page with source = Some source }
   let withTarget target page =
       { page with target = Some target }
   let fromStream stream = 
       { content = stream
         target = None
         source = None
         meta = ()
       }