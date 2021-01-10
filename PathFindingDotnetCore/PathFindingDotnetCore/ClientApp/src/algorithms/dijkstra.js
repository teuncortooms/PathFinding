export function dijkstraAnalyse(grid, startNode, destinationNode) {
    if (!startNode || !destinationNode || startNode === destinationNode) return false; 

    const visitedNodesInOrder = [];
    startNode.distance = 0;
    const unvisitedNodes = getAllNodes(grid);
    let foundHim = false;
    while (unvisitedNodes.length !== 0 && !foundHim) {
        sortNodesByDistance(unvisitedNodes);
        const closestNode = unvisitedNodes.shift();
        if (closestNode.isWall) continue;
        closestNode.isVisited = true;
        visitedNodesInOrder.push(closestNode);
        if (closestNode === destinationNode) foundHim = true;
        else updateUnvisitedNeighbours(closestNode, grid);
    }
    return visitedNodesInOrder;
}

function getAllNodes(grid) {
    const nodes = [];
    for (const row of grid) {
        for (const node of row) {
            nodes.push(node);
        }
    }
    return nodes;
}

function sortNodesByDistance(unvisitedNodes) {
    unvisitedNodes.sort((nodeA, nodeB) => nodeA.distance - nodeB.distance);
}

function updateUnvisitedNeighbours(node, grid) {
    const unvisitedNeighbours = getUnvisitedNeighbours(node, grid);
    for (const neighbour of unvisitedNeighbours) {
        neighbour.distance = node.distance + 1;
        neighbour.previousNode = node;
    }
}

function getUnvisitedNeighbours(node, grid) {
    const neighbours = [];
    const { col, row } = node;
    if (row > 0) neighbours.push(grid[row - 1][col]); // add above
    if (row < grid.length - 1) neighbours.push(grid[row + 1][col]); // add below
    if (col > 0) neighbours.push(grid[row][col - 1]); // add left
    if (col < grid[0].length - 1) neighbours.push(grid[row][col + 1]); // add right
    return neighbours.filter(neighbour => !neighbour.isVisited);
}

export function getDijkstraReport(visitedNodesInOrder) {
    const visitedInOrder = getVisitedIdsInOrder(visitedNodesInOrder);
    const shortestPathToDest = getPath(visitedNodesInOrder);
    return { visitedInOrder, shortestPathToDest };
}

function getVisitedIdsInOrder(visitedNodesInOrder) {
    const visitedIds = [];
    for (let i = 0; i < visitedNodesInOrder.length; i++) {
        visitedIds[i] = visitedNodesInOrder[i].id;
    }
    return visitedIds;
}

function getPath(visitedNodesInOrder) {
    const path = [];
    const last = visitedNodesInOrder.length - 1;
    let node = visitedNodesInOrder[last];
    if (!node.isDestination) return false;

    while (!node.isStart) {
        path.push(node.id);
        node = node.previousNode;
    }
    path.push(node.id);
    return path;
}