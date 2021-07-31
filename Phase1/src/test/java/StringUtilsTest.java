import org.junit.jupiter.api.Test;

import java.util.Arrays;
import java.util.TreeSet;

import static org.junit.jupiter.api.Assertions.assertEquals;

class StringUtilsTest {
    @Test
    public void removeStopWords() {
        TreeSet<String> wordsWithStopWord = new TreeSet<>(Arrays.asList("hello", "this", "my", "iran"));
        StringUtils.removeStopWords(wordsWithStopWord);
        TreeSet<String> expected = new TreeSet<>(Arrays.asList("hello", "iran"));
        assertEquals(expected, wordsWithStopWord);
    }

    @Test
    public void stem() {
        TreeSet<String> toStemSet = new TreeSet<>(Arrays.asList("cats", "helpful", "hello"));
        TreeSet<String> stemmedSet = StringUtils.stem(toStemSet);
        TreeSet<String> expected = new TreeSet<>(Arrays.asList("cat", "help", "hello"));
        assertEquals(expected, stemmedSet);
    }


    @Test
    public void processWords() {
        TreeSet<String> toBeProcess = new TreeSet<>(Arrays.asList("cats", "this", "hello", "helpful"));
        TreeSet<String> processedWords = StringUtils.processRawTokens(toBeProcess);
        TreeSet<String> expected = new TreeSet<>(Arrays.asList("cat", "hello", "help"));
        assertEquals(expected, processedWords);
    }
}