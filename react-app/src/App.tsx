import { QueryClient, QueryClientProvider } from "react-query";
import { HomePage } from "./pages/HomePage";

export const App = () => {
  const queryClient = new QueryClient();

  return (
    <QueryClientProvider client={queryClient}>
      <HomePage />
    </QueryClientProvider>
  );
};
