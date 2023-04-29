import { QueryClient, QueryClientProvider } from "react-query";
import { HomePage } from "./pages/HomePage";
import { Box } from "@mui/material";

export const App = () => {
  const queryClient = new QueryClient();

  return (
    <QueryClientProvider client={queryClient}>
      <Box
        sx={{
          display: "flex",
          justifyContent: "center",
        }}
      >
        <Box
          sx={{ width: "100%", maxWidth: "min(100%, 1024px)", margin: "1rem" }}
        >
          <HomePage />
        </Box>
      </Box>
    </QueryClientProvider>
  );
};
