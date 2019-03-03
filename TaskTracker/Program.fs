module Main

open System
open System.Text.RegularExpressions
open Infrastructure

type Commands =
  | HelpCommand
  | LogCommand
  | ListCommand of Group option
  | ExitCommand
  | Unrecognized
    
let (|Regex|_|) pattern input = 
  let m = Regex.Match(input, pattern)
  if m.Success then Some(List.tail [for g in m.Groups -> g.Value])
  else None

let matchCommand input =
  match input with
  | Regex @"^\s*(help|\/\?)\s*$" [_] ->
    HelpCommand
  | Regex @"^\s*log\s*$" [] ->
    LogCommand
  | Regex @"^\s*list(\s+(\d{1,3}))?\s*$" [_; index] ->
    ListCommand (groupOpt index)
  | _ -> Unrecognized

[<EntryPoint>]
let main argv = 
  Seq.initInfinite (fun _ -> Console.ReadLine())
  |>  Seq.takeWhile (fun line -> String.length line > 0)
  |>  Seq.iter(fun line -> printfn "%A" (matchCommand line))
  |> ignore
  0

