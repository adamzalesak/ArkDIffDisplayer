import { Box, Typography } from "@mui/material";
import { DataDiffItem } from "../models";
import { Loading } from "./Loading";
import { PositionsTable } from "./PositionsTable";

export const Positions = ({
  positions,
  title,
  isLoading,
}: {
  positions?: DataDiffItem[];
  title: string;
  isLoading?: boolean;
}) => {
  return (
    <Box sx={{ marginX: "1rem", marginY: "2rem" }}>
      <Typography variant="h2">{title}</Typography>
      {isLoading ? <Loading /> : <PositionsTable positions={positions ?? []} />}
    </Box>
  );
};
