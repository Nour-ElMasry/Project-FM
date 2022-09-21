import React, { useState } from 'react';
import Table from '@mui/material/Table';
import TableBody from '@mui/material/TableBody';
import TableCell from '@mui/material/TableCell';
import TableHead from '@mui/material/TableHead';
import TableRow from '@mui/material/TableRow';
import ExpandMoreIcon from '@mui/icons-material/ExpandMore';
import { styled } from '@mui/material/styles';
import IconButton from '@mui/material/IconButton';


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

    const tableExpand = () => {
        if(expanded) {
            return <>
                {props.league.map((l, i) => {
                return <TableRow hover className='leagueTableRow' key={i}>
                    <TableCell className='leagueTableTeam'>
                        <img src={l.homeTeam.teamLogo} alt='teamLogo'></img>
                        <p>{l.homeTeam.teamName}</p>
                    </TableCell>
                    <TableCell sx={{textAlign: 'center', padding: '0'}}>
                        {l.isPlayed === true ? 
                            <h3>{l.fixtureScore.homeScore} - {l.fixtureScore.awayScore}</h3> 
                            : <h3>vs</h3>}
                    </TableCell>
                    <TableCell className='leagueTableTeam'>
                        <img src={l.awayTeam.teamLogo} alt='teamLogo'></img>
                        <p>{l.awayTeam.teamName}</p>
                    </TableCell>
                </TableRow>
                })}
            </>
        }
        return <>
            {props.league
            .slice(0, 5)
            .map((l, i) => {
                return <TableRow hover className='leagueTableRow' key={i}>
                    <TableCell className='leagueTableTeam'>
                        <img src={l.homeTeam.teamLogo} alt='teamLogo'></img>
                        <p>{l.homeTeam.teamName}</p>
                    </TableCell>
                    <TableCell sx={{textAlign: 'center', padding: '0'}}>
                    {l.isPlayed === true ? 
                            <h3>{l.fixtureScore.homeScore} - {l.fixtureScore.awayScore}</h3> 
                            : <h3>vs</h3>}
                    </TableCell>
                    <TableCell className='leagueTableTeam'>
                        <img src={l.awayTeam.teamLogo} alt='teamLogo'></img>
                        <p>{l.awayTeam.teamName}</p>
                    </TableCell>
                </TableRow>
            })}
        </>
    }
    return <Table className='leagueTable'>
        <TableHead>
           <TableRow>
                <TableCell colSpan={3}>
                    <p className='title'>{props.league[0].fixtureLeague.leagueName}</p>
                </TableCell>
           </TableRow>
        </TableHead>
        <TableBody>
            {tableExpand()}
            <TableRow>
                <TableCell className='expandMoreRow' colSpan={3}>
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