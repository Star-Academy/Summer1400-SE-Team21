import org.junit.jupiter.api.Assertions;
import org.junit.jupiter.api.Test;

import java.io.ByteArrayInputStream;
import java.io.ByteArrayOutputStream;
import java.io.PrintStream;
import java.util.Arrays;
import java.util.TreeSet;

import static org.junit.jupiter.api.Assertions.*;

class UserInputTest {
    @Test
    public void processUserString(){
        String input = "hello this +fine -cats";
        UserInput userInput = new UserInput(input);

        TreeSet<String> expectedAndInput = new TreeSet<>(Arrays.asList("hello"));
        TreeSet<String> expectedOrInput = new TreeSet<>(Arrays.asList("fine"));
        TreeSet<String> expectedRemoveInput = new TreeSet<>(Arrays.asList("cat"));

        assertEquals(expectedAndInput,userInput.getAndInputs());
        assertEquals(expectedOrInput,userInput.getOrInputs());
        assertEquals(expectedRemoveInput,userInput.getRemoveInputs());
    }


    @Test
    public void queryTest (){
        FileReader fileReader = new FileReader();
        InvertedIndex invertedIndex = new InvertedIndex().tokenizeFiles(fileReader.readingFiles("src/test/DocsForTest"));

        UserInput userInput = new UserInput("everyone");
        TreeSet<String> files = invertedIndex.query(userInput);
        Assertions.assertFalse(files.isEmpty());
        Assertions.assertEquals(files.first(), "firstFile");
        Assertions.assertEquals(1, files.size());
    }


    @Test
    public void queryTest2 (){
        FileReader fileReader = new FileReader();
        InvertedIndex invertedIndex = new InvertedIndex().tokenizeFiles(fileReader.readingFiles("src/test/DocsForTest"));

        UserInput userInput = new UserInput("everyone +hello everyone -cat");
        TreeSet<String> files = invertedIndex.query(userInput);
        Assertions.assertTrue(files.isEmpty());
    }


    @Test
    public void queryTest3 (){
        FileReader fileReader = new FileReader();
        InvertedIndex invertedIndex = new InvertedIndex().tokenizeFiles(fileReader.readingFiles("src/test/DocsForTest"));

        UserInput userInput = new UserInput("dog");
        TreeSet<String> files = invertedIndex.query(userInput);
        Assertions.assertTrue(files.isEmpty());
    }


    @Test
    public void mainTest (){
        ByteArrayInputStream in2 = new ByteArrayInputStream("cat\nnothing\nexit".getBytes());
        System.setIn(in2);

        ByteArrayOutputStream outputStream = new ByteArrayOutputStream();
        System.setOut(new PrintStream(outputStream));

        String[] args = new String[1];
        args[0] = "who";
        Main.main(args);
        assertEquals("no doc found\nno doc found\n",outputStream.toString().replaceAll("\r",""));
    }


    @Test
    public void positiveTest (){
        FileReader fileReader = new FileReader();
        InvertedIndex invertedIndex = new InvertedIndex().tokenizeFiles(fileReader.readingFiles("src/test/DocsForTest"));

        UserInput userInput = new UserInput("+elephant");
        TreeSet<String> files = invertedIndex.query(userInput);
        Assertions.assertTrue(files.isEmpty());
    }


    @Test
    public void minusTest (){
        FileReader fileReader = new FileReader();
        InvertedIndex invertedIndex = new InvertedIndex().tokenizeFiles(fileReader.readingFiles("src/test/DocsForTest"));

        UserInput userInput = new UserInput("-elephant");
        TreeSet<String> files = invertedIndex.query(userInput);
        Assertions.assertTrue(files.isEmpty());
    }


    @Test
    public void emptyTest (){
        FileReader fileReader = new FileReader();
        InvertedIndex invertedIndex = new InvertedIndex().tokenizeFiles(fileReader.readingFiles("src/test/DocsForTest"));

        UserInput userInput = new UserInput(" ");
        TreeSet<String> files = invertedIndex.query(userInput);
        Assertions.assertTrue(files.isEmpty());
    }
}