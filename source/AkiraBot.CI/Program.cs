using AkiraBot.CI;

var cnsl = new StartUp();
cnsl.PrintStartUpMessage();
cnsl.CheckFoldersExists();
cnsl.ReadCommands();