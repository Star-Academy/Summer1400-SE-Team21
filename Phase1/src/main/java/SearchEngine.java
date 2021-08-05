import java.io.FileWriter;
import java.util.TreeSet;

public class SearchEngine {
    private final static String DOC_PATH = "Docs";

    public void run(){
        FileReader fileReader = new FileReader();
        InvertedIndex invertedIndex = new InvertedIndex().tokenizeFiles(fileReader.readingFiles(DOC_PATH));

        while (true){
            String input = reader.read();
            if(input.equals("exit"))
                break;
            UserInput userInput = new UserInput(input);
            TreeSet<String> containingDocs = invertedIndex.query(userInput);
            if(containingDocs.isEmpty())
                writer.write("no doc found");
            for(String docName:containingDocs)
                writer.write(docName);
        }
    }
}
