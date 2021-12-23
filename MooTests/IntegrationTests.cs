using Microsoft.EntityFrameworkCore;
using PerhapsAGame.Core.DataAccess;
using PerhapsAGame.Core.GameBase;
using PerhapsAGame.Core.Moo;
using PerhapsAGame.Services;
using System;
using System.Collections.Generic;
using Xunit;

namespace MooTests;

public class IntegrationTests 
{
    public static MooState? state = new();
    MooGameController ctrl;
    TestOutput testOutput;
    public IntegrationTests()
    {
        var options = new DbContextOptionsBuilder<SQLiteContext>()
                 .UseInMemoryDatabase("TestDatabase")
                 .Options;
        var context = new SQLiteContext(options);
        var service = new ScoreService(context);

        testOutput = new TestOutput();
        var input = new TestInput();


        var logic = new GameManager(service, input, testOutput, state);
        ctrl = new MooGameController(logic);
    }

    [Fact]
    public void SetTest()
    {
        ctrl.StartMoo();

        Assert.NotNull(() => ctrl.StartMoo());
        Assert.Equal("BBBB,", testOutput.output[4]);
        Assert.Contains("BBB,", testOutput.output[2]);
    }
}

public class TestInput : IInputProvider
{
    Stack<string> input = new();
    public TestInput()
    {
    }
    public string Read()
    {
        var result = string.Join("", IntegrationTests.state.Target);
        int intResult;
        Int32.TryParse(string.Join("", IntegrationTests.state.Target), out intResult); 
        if (result != null && !input.Contains("n"))
        {
            input.Push("n");
            input.Push(result);
            input.Push(string.Join("", intResult -1));
            input.Push("BabyWhiskers");
        }
        return input.Pop();
    }
}

public class TestOutput : IOutputProvider
{
    public List<string> output = new();
    public void Write(string message)
    {
        output.Add(message);
    }
}
