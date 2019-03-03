module Main

open System
open Commands

[<EntryPoint>]
let main argv = 
  Seq.initInfinite (fun _ -> Console.ReadLine())
  |>  Seq.takeWhile (fun line -> String.length line > 0)
  |>  Seq.iter(fun line -> printfn "%A" (matchCommand line))
  |> ignore
  0

