import React, { useState } from 'react';
import Table from '@mui/material/Table';
import TableBody from '@mui/material/TableBody';
import TableCell from '@mui/material/TableCell';
import TableHead from '@mui/material/TableHead';
import TableRow from '@mui/material/TableRow';
import ExpandMoreIcon from '@mui/icons-material/ExpandMore';
import { styled } from '@mui/material/styles';
import IconButton from '@mui/material/IconButton';
import MatchTableItem from './MatchTableItem';


const ExpandMore = styled((props) => {
    const { expand, ...other } = props;
    return <IconButton {...other} />;
  })(({ theme, expand }) => ({
    transform: !expand ? 'rotate(0deg)' : 'rotate(180deg)',
    marginLeft: 'auto',
    transition: theme.transitions.create('transform', {
      duration: theme.transitions.duration.shortest,
    }),
  }));

const LeagueMatchesTable = (props) => {
    const [expanded, setExpanded] = useState(false);

    const handleExpandClick = () => {
        setExpanded(!expanded);
    };

    const tableDisplay = () => {
        if(expanded) {
            return <>
                {props.league.map((l, i) => {
                return  <MatchTableItem match={l} key={i} />;
                })}
            </>
        }
        return <>
            {props.league
            .slice(0, 5)
            .map((l, i) => {
                return <MatchTableItem match={l} key={i} />;
            })}
        </>
    }
    return <Table className='leagueTableMatch'>
        <TableHead>
           <TableRow>
                <TableCell>
                    <p className='table-title'>{props.league[0].fixtureLeague.leagueName}</p>
                </TableCell>
           </TableRow>
        </TableHead>
        <TableBody>
            {tableDisplay()}
            <TableRow>
                <TableCell className='expandMoreRow'>
                    <ExpandMore
                        expand={expanded}
                        onClick={handleExpandClick}
                        aria-expanded={expanded}
                        aria-label="show more"
                        >
                        <ExpandMoreIcon />
                    </ExpandMore>
                </TableCell>
            </TableRow>
        </TableBody>
    </Table>
}

export default LeagueMatchesTable;