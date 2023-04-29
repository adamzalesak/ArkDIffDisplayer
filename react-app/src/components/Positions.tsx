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
    <Box sx={{ marginY: "2rem" }}>
      <Typography
        sx={{ fontSize: { xs: "2rem", sm: "2.5rem", md: "3rem" } }}
        variant="h2"
      >
        {title}
      </Typography>
      {isLoading ? <Loading /> : <PositionsTable positions={positions ?? []} />}
    </Box>
  );
};
