import org.junit.jupiter.api.Assertions;
import org.junit.jupiter.api.BeforeAll;
import org.junit.jupiter.api.Test;

import java.util.List;
import java.util.TreeSet;

import static org.junit.jupiter.api.Assertions.assertEquals;
import static org.junit.jupiter.api.Assertions.assertTrue;

class InvertedIndexTest {
    private static InvertedIndex invertedIndex;

    @BeforeAll
    public static void init() {
        invertedIndex = new InvertedIndex();
        invertedIndex.tokenizeFiles(new FileReader().readingFiles("src/test/testDocs"));
    }

    @Test
    public void testSimpleInvertedIndex() {
        TreeSet<String> queryResult = invertedIndex.query(new UserInput("Cause +People -pretty"));
        TreeSet<String> expected = new TreeSet<>(List.of("59631.txt", "59635.txt", "59639.txt", "59648.txt"));
        assertEquals(expected,queryResult);
    }

    @Test
    void testJustAndQuery() {
        TreeSet<String> result = invertedIndex.query(new UserInput("Cause People pretty"));
        assertTrue(result.isEmpty());
    }

    @Test
    void testJustOrQuery(){
        TreeSet<String> queryResult = invertedIndex.query(new UserInput("+Cause +People +pretty"));
        TreeSet<String> expected = new TreeSet<>(List.of("59631.txt", "59632.txt",
                "59633.txt", "59635.txt",
                "59637.txt", "59639.txt", "59648.txt"));
        assertEquals(expected,queryResult);
    }

    @Test
    void testJustRemoveQuery(){
        TreeSet<String> result = invertedIndex.query(new UserInput("-Cause -People -pretty"));
        assertTrue(result.isEmpty());
    }

    @Test
    void testEmptyQuery() {
        TreeSet<String> result = invertedIndex.query(new UserInput(""));
        assertTrue(result.isEmpty());
    }

    @Test
    void testLongQuery(){
        TreeSet<String> result = invertedIndex.query(new UserInput("thisWordIsTooLongAndThereShouldBeNoMatchDocInOurDatabase"));
        assertTrue(result.isEmpty());
    }


    @Test
    public void invertedIndexSimpleTest (){
        FileReader fileReader = new FileReader();
        InvertedIndex invertedIndex = new InvertedIndex().tokenizeFiles(fileReader.readingFiles("src/test/DocsForTest"));

        Assertions.assertTrue(invertedIndex.tokenizedWords.get("hello").contains("firstFile"));
        Assertions.assertNull(invertedIndex.tokenizedWords.get("ali"));
    }

}