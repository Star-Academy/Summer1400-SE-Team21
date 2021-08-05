import java.io.FileWriter;
import java.util.TreeSet;

public class SearchEngine {
    private final static String DOC_PATH = "Docs";
    private Reader reader;
    private Writer writer;

    public SearchEngine(Reader reader,Writer writer){
        this.reader = reader;
        this.writer = writer;
    }

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
