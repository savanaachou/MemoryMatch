using UnityEngine;
using System.Collections.Generic;

public class CommandInvoker
{
    private Stack<ICommand> commandHistory = new Stack<ICommand>();

    public void ExecuteCommand(ICommand command)
    {
        Debug.Log($"[CommandInvoker] Executing command: {command.GetType().Name}");

        command.Execute();
        commandHistory.Push(command);
    }

    public void UndoLastCommand()
    {
        if (commandHistory.Count == 0)
        {
            Debug.Log("[CommandInvoker] Nothing to undo.");
            return;
        }

        ICommand lastCommand = commandHistory.Pop();
        Debug.Log($"[CommandInvoker] Undoing command: {lastCommand.GetType().Name}");

        lastCommand.Undo();
    }
}