import org.junit.jupiter.api.Test;

import java.io.ByteArrayInputStream;
import java.io.ByteArrayOutputStream;
import java.io.PrintStream;

import static org.junit.jupiter.api.Assertions.assertEquals;

class SearchEngineTest {
    @Test
    void testSearchEngine() {
        ByteArrayInputStream inputStream = new ByteArrayInputStream("hello\nthis\nexit".getBytes());
        System.setIn(inputStream);
        ByteArrayOutputStream outputStream = new ByteArrayOutputStream();
        System.setOut(new PrintStream(outputStream));
        new SearchEngine("").run();
        assertEquals("no doc found\nno doc found\n",outputStream.toString().replaceAll("\r",""));
    }
}