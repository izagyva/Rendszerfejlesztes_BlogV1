import { postData, getData } from './api.js';

async function onLoadTopics() {
    const topics = await getTopics(1, 10); // replace with your actual page number and page size
    const topicListElement = document.getElementById('topic-list');
    topics.forEach(topic => {
        const topicElement = document.createElement('div');
        topicElement.textContent = topic.title; // replace with your actual topic title property
        topicListElement.appendChild(topicElement);
    });
}


async function getTopics(pageNumber, pageSize) {
    const response = await getData(`topics?pageNumber=${pageNumber}&pageSize=${pageSize}`);
    if (response.status === 200) {
        return response.json();
    } else {
        console.error('Error getting topics:', response);
    }
}

async function getTopic(id) {
    const response = await getData(`topics/${id}`);
    if (response.status === 200) {
        return response.json();
    } else {
        console.error('Error getting topic:', response);
    }
}

async function getUserTopics() {
    const response = await getData('topics/user');
    if (response.status === 200) {
        return response.json();
    } else {
        console.error('Error getting user topics:', response);
    }
}

async function populateTopicTypes() {
    const topicTypes = await getTopicTypes();
    const dropdown = document.getElementById('topicTypeFilter');
    topicTypes.forEach(type => {
        const option = document.createElement('option');
        option.value = type.id;
        option.text = type.name;
        dropdown.add(option);
    });
}

async function onFilterChange() {
    const selectedType = document.getElementById('topicTypeFilter').value;
    const topics = await getTopicsByType(selectedType);
    const topicListElement = document.getElementById('topic-list');
    // Clear the current list of topics
    topicListElement.innerHTML = '';
    topics.forEach(topic => {
        const topicElement = document.createElement('div');
        topicElement.textContent = topic.title; // replace with your actual topic title property
        topicListElement.appendChild(topicElement);
    });
}

export { getTopics, getTopic, getUserTopics };
