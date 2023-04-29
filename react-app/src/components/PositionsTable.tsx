import {
  Paper,
  Table,
  TableBody,
  TableCell,
  TableContainer,
  TableHead,
  TableRow,
  styled,
  tableCellClasses,
} from "@mui/material";
import { DataDiffItem } from "../models";

const StyledTableCell = styled(TableCell)(({ theme }) => ({
  [`&.${tableCellClasses.head}`]: {
    color: theme.palette.common.white,
    backgroundColor: theme.palette.common.black,
  },
  [`&.${tableCellClasses.body}`]: {
    fontSize: 14,
  },
}));

export const PositionsTable = ({
  positions,
}: {
  positions: DataDiffItem[];
}) => {
  return (
    <TableContainer component={Paper}>
      <Table>
        <TableHead>
          <TableRow>
            <StyledTableCell>Company</StyledTableCell>
            <StyledTableCell>Ticker</StyledTableCell>
            <StyledTableCell align="right">Shares</StyledTableCell>
            <StyledTableCell align="right">
              Shares Percentage Change
            </StyledTableCell>
            <StyledTableCell align="right">Weight Percentage</StyledTableCell>
          </TableRow>
        </TableHead>
        <TableBody>
          {positions.map((x) => (
            <TableRow key={x.companyName}>
              <StyledTableCell>{x.companyName}</StyledTableCell>
              <StyledTableCell>{x.ticker}</StyledTableCell>
              <StyledTableCell align="right">{x.shares}</StyledTableCell>
              <StyledTableCell align="right">
                {x.sharesPercentageChange}
              </StyledTableCell>
              <StyledTableCell align="right">
                {x.weightPercentage}
              </StyledTableCell>
            </TableRow>
          ))}
        </TableBody>
      </Table>
    </TableContainer>
  );
};
