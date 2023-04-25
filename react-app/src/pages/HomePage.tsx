import { Typography } from "@mui/material";
import { useDataDiff } from "../api";
import { Positions } from "../components/Positions";

export const HomePage = () => {
  const { data, isLoading } = useDataDiff();

  return (
    <>
      <Typography variant="h1" sx={{ textAlign: "center" }}>
        ArkDiffDisplayer
      </Typography>

      <Positions
        title="New positions"
        positions={data?.newPositions}
        isLoading={isLoading}
      />
      <Positions
        title="Increased Positions"
        positions={data?.increasedPositions}
        isLoading={isLoading}
      />
      <Positions
        title="Reduced Positions"
        positions={data?.reducedPositions}
        isLoading={isLoading}
      />
    </>
  );
};
