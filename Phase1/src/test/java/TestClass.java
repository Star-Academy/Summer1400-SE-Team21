import org.junit.jupiter.api.Assertions;
import org.junit.jupiter.api.Test;

import java.io.ByteArrayInputStream;
import java.util.HashMap;
import java.util.TreeSet;

public class TestClass {
    @Test
    public void gettingFileAndReadIt (){
        FileReader fileReader = new FileReader();
        HashMap<String, String> files = fileReader.readingFiles("src/test/DocsForTest");

        Assertions.assertTrue(files.containsKey("firstFile"));
        Assertions.assertTrue(files.get("firstFile").startsWith("Hello Everyone"));
    }


    @Test
    public void invertedIndexSimpleTest (){
        FileReader fileReader = new FileReader();
        InvertedIndex invertedIndex = new InvertedIndex().tokenizeFiles(fileReader.readingFiles("src/test/DocsForTest"));

        Assertions.assertTrue(invertedIndex.tokenizedWords.get("hello").contains("firstFile"));
        Assertions.assertNull(invertedIndex.tokenizedWords.get("ali"));
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
    public void searchEngineRun (){
        ByteArrayInputStream in2 = new ByteArrayInputStream("cat\nnothing\nexit".getBytes());
        System.setIn(in2);

        SearchEngine searchEngine = new SearchEngine();
        searchEngine.run();
    }


    @Test
    public void stemmerTest (){
        FileReader fileReader = new FileReader();
        InvertedIndex invertedIndex = new InvertedIndex().tokenizeFiles(fileReader.readingFiles("src/test/DocsForTest"));

        UserInput userInput = new UserInput("played actional national enci anci izer bli alli entli eli " +
                "ousli ization ation ator alism iveness fulness ousness aliti iviti biliti logi icate ative alize " +
                "iciti ical ful ness er ic iti ive ize");
        TreeSet<String> files = invertedIndex.query(userInput);
        Assertions.assertTrue(files.isEmpty());
    }


    @Test
    public void stemmerTest2 (){
        FileReader fileReader = new FileReader();
        InvertedIndex invertedIndex = new InvertedIndex().tokenizeFiles(fileReader.readingFiles("src/test/DocsForTest"));

        UserInput userInput = new UserInput("hello eed piuic");
        TreeSet<String> files = invertedIndex.query(userInput);
        Assertions.assertTrue(files.isEmpty());
    }


    @Test
    public void mainTest (){
        ByteArrayInputStream in2 = new ByteArrayInputStream("cat\nnothing\nexit".getBytes());
        System.setIn(in2);

        String[] args = new String[1];
        args[0] = "who";
        Main.main(args);
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
    public void exceptionTest (){
        FileReader fileReader = new FileReader();
        try {
            fileReader.readingFiles("noWhere");
        }catch (Exception e){
            e.printStackTrace();
        }
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
