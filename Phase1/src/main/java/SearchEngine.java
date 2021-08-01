import java.util.Scanner;
import java.util.TreeSet;

public class SearchEngine {
    private final static String DEFAULT_DOC_PATH = "Docs";
    private String docPath;

    public SearchEngine(){
        docPath = DEFAULT_DOC_PATH;
    }

    public SearchEngine(String docPath){
        this.docPath = docPath;
    }

    public void run(){
        FileReader fileReader = new FileReader();
        InvertedIndex invertedIndex = new InvertedIndex().tokenizeFiles(fileReader.readingFiles(docPath));

        Scanner scanner = new Scanner(System.in);

        while (true){
            String input = scanner.nextLine();
            if(input.equals("exit"))
                break;
            UserInput userInput = new UserInput(input);
            TreeSet<String> containingDocs = invertedIndex.query(userInput);
            if(containingDocs.isEmpty())
                System.out.println("no doc found");
            for(String docName:containingDocs)
                System.out.println(docName);
        }
    }
}
