import TableCell from "@mui/material/TableCell";
import RatingSpan from "./RatingSpan";
const RatingTableCell = (props) => {
  return (
    <TableCell className={props.hide && "tableColumnHide"} align="center">
      <RatingSpan overall={props.overall} rating={props.rating} />
    </TableCell>
  );
};

export default RatingTableCell;
