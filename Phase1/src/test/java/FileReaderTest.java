import org.junit.jupiter.api.Test;

import java.util.HashMap;

import static org.junit.jupiter.api.Assertions.assertEquals;

class FileReaderTest {
    @Test
    public void readNotAvailableFolder() {
        FileReader fileReader = new FileReader();
        HashMap<String,String> contents = fileReader.readingFiles("notAvailable");
        HashMap<String,String> expected = new HashMap<>();
        assertEquals(expected,contents);
    }

    @Test
    public void readSimpleFolder(){
        FileReader fileReader = new FileReader();
        HashMap<String,String> content = fileReader.readingFiles("src/test/simpleFolder");
        HashMap<String,String> expected = new HashMap<>();
        expected.put("simpleFile1.txt","this is simpleFile1");
        expected.put("simpleFile2.txt","this is simpleFile2");
        assertEquals(expected,content);
    }

    @Test
    public void readComplexFolder(){
        FileReader fileReader = new FileReader();
        HashMap<String,String> content = fileReader.readingFiles("src/test/complexFolder");
        HashMap<String,String> expected = new HashMap<>();
        expected.put("childFile1.txt","this is childFile1");
        expected.put("childFile2.txt","this is childFile2");
        expected.put("complexFile1.txt","this is complexFile1");
        expected.put("complexFile2.txt","this is complexFile2");
        assertEquals(expected,content);
    }
}