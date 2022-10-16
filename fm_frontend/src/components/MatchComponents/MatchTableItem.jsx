import TableCell from '@mui/material/TableCell';
import TableRow from '@mui/material/TableRow';
import { Link } from 'react-router-dom';
import MatchItem from './MatchItem';

const MatchTableItem = (props) => {
    return <TableRow hover className='matchesTableRow'>
    <TableCell>
        <Link to={'/matches/'+props.match.id}>
            <MatchItem tableView={true} match={props.match} />
        </Link>
    </TableCell>
</TableRow>
}


export default MatchTableItem;