module Commands

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
