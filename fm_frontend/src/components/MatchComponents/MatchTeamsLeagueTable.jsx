import React, { useEffect, useState } from 'react';
import GeneralAxiosService from '../../services/GeneralAxiosService';
import Box from '@mui/material/Box';
import CircularProgress from '@mui/material/CircularProgress';
import Table from '@mui/material/Table';
import TableBody from '@mui/material/TableBody';
import TableCell from '@mui/material/TableCell';
import TableHead from '@mui/material/TableHead';
import TableRow from '@mui/material/TableRow';
import LeagueTableTeam from '../LeagueComponents/LeagueTableTeam';

const MatchTeamsLeagueTable = (props) => {
    const [leagueTeams, setLeagueTeams] = useState({})
    const [loading, setLoading] = useState(true)

    const league = props.league
    const homeTeam = props.homeTeam
    const awayTeam = props.awayTeam

    useEffect(() => {
        GeneralAxiosService.getMethod('https://localhost:7067/api/v1/Leagues/'+league.leagueId+'/Teams/0')
        .then((response) => 
        setLeagueTeams(
            response.data.sort((a, b) => 
                b.currentSeasonStats.points - a.currentSeasonStats.points
        ).sort((a, b) => 
            (b.currentSeasonStats.goalsFor - b.currentSeasonStats.goalsAgainst)
            -
            (a.currentSeasonStats.goalsFor - a.currentSeasonStats.goalsAgainst)
        )))
        .then(() => setLoading(false))
    },[])

    return <div>
        {loading && <Box sx={{textAlign: "center", marginTop: "10rem"}}>
        <CircularProgress style={ {width: "3rem", height: "3rem"}} />
        </Box>}
        
        {!loading && <Table className='leagueTable'>
                <TableHead>
                    <TableRow>
                        <TableCell colSpan={4}>
                            <p>{league.leagueName}</p>
                        </TableCell>
                        <TableCell align="right">
                            <p>PL</p>
                        </TableCell>
                        <TableCell className='tableColumnHide' align="right">
                            <p>W</p>
                        </TableCell>
                        <TableCell className='tableColumnHide' align="right">
                            <p>D</p>
                        </TableCell>
                        <TableCell className='tableColumnHide' align="right">
                            <p>L</p>
                        </TableCell>
                        <TableCell align="right">
                            <p>GD</p>
                        </TableCell>
                        <TableCell align="right">
                            <p>PTS</p>
                        </TableCell>
                    </TableRow>
                </TableHead>
                <TableBody>
                    <LeagueTableTeam 
                    team={leagueTeams[leagueTeams.findIndex(t => 
                            t.id === homeTeam.teamId &&
                            t.name === homeTeam.teamName &&
                            t.logo === homeTeam.teamLogo)]} 
                    position={1 + leagueTeams.findIndex(t => 
                            t.id === homeTeam.teamId &&
                            t.name === homeTeam.teamName &&
                            t.logo === homeTeam.teamLogo)}
                     />
                    <LeagueTableTeam 
                    team={leagueTeams[leagueTeams.findIndex(t => 
                            t.id === awayTeam.teamId &&
                            t.name === awayTeam.teamName &&
                            t.logo === awayTeam.teamLogo)]} 
                    position={1 + leagueTeams.findIndex(t => 
                            t.id === awayTeam.teamId &&
                            t.name === awayTeam.teamName &&
                            t.logo === awayTeam.teamLogo)}
                     />
                </TableBody>
            </Table>}
    </div>
}

export default MatchTeamsLeagueTable;
