import org.junit.jupiter.api.Assertions;
import org.junit.jupiter.api.Test;

import java.util.TreeSet;

public class StemmerTest {
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
}
