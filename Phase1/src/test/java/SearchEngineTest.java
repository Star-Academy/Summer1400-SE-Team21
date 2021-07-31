import org.junit.jupiter.api.Test;

import java.io.ByteArrayInputStream;

class SearchEngineTest {
    @Test
    void testSearchEngine() {
        ByteArrayInputStream inputStream = new ByteArrayInputStream("hello\nthis\nexit".getBytes());
        System.setIn(inputStream);
        new SearchEngine().run();
    }
}