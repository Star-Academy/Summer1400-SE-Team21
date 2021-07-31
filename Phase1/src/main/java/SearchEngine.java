import java.util.Scanner;
import java.util.TreeSet;

public class SearchEngine {
    private final static String DOC_PATH = "Docs";

    public void run(){
        InvertedIndex invertedIndex = new InvertedIndex().tokenizeFiles(DOC_PATH);

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
