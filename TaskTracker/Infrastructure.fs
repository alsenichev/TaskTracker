module Infrastructure
open System

// Regex group
type Group = private Group of string
let groupOpt candidate = 
  if String.IsNullOrWhiteSpace candidate then
    None
  else
    Some (Group candidate)


