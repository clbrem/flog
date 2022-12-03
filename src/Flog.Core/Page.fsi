namespace Flog.Core

type Page<'T,'I,'O> 

module Page =  
   open System.Threading.Tasks
   val render : path: ('O -> string) -> Page<'T,'I,'O> -> Task