import TableCell from '@mui/material/TableCell';
const RatingTableCell = (props) => {
    const ratingIndicator = () => {
        if(props.rating < 50)
            return <span className='playerRating playerRating__red'>{props.rating}</span>
        else if(props.rating < 80)
            return  <span className='playerRating playerRating__yellow'>{props.rating}</span>

            return  <span className='playerRating playerRating__green'>{props.rating}</span>
    }
    return <TableCell className='tableColumnHide' align="center">
       {ratingIndicator()}
    </TableCell>
}

export default RatingTableCell;