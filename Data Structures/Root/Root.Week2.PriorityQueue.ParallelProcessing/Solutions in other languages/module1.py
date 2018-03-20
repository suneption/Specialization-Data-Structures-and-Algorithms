# python3
import heapq

class MyHeap(object):
   def __init__(self, initial=None, key=lambda x:x):
       self.key = key
       if initial:
           self._data = [(key(item), item) for item in initial]
           heapq.heapify(self._data)
       else:
           self._data = []

   def push(self, item):
       heapq.heappush(self._data, (self.key(item), item))

   def pop(self):
       return heapq.heappop(self._data)[1]

class JobQueue:
    def read_data(self):
        self.num_workers, m = map(int, input().split())
        self.jobs = list(map(int, input().split()))
        assert m == len(self.jobs)

    def write_response(self):
        for i in range(len(self.jobs)):
            (thread, startTime) = self.result[i]
            print(thread, startTime)

        #for i in range(len(self.jobs)):
        #  print(self.assigned_workers[i], self.start_times[i]) 

    def simpleSolve(self):
        # TODO: replace this code with a faster algorithm.
        self.assigned_workers = [None] * len(self.jobs)
        self.start_times = [None] * len(self.jobs)
        next_free_time = [0] * self.num_workers
        for i in range(len(self.jobs)):
          next_worker = 0
          for j in range(self.num_workers):
            if next_free_time[j] < next_free_time[next_worker]:
              next_worker = j
          self.assigned_workers[i] = next_worker
          self.start_times[i] = next_free_time[next_worker]
          next_free_time[next_worker] += self.jobs[i]

    def assign_jobs(self):
        availableThreads = []
        for i in range(self.num_workers):
            heapq.heappush(availableThreads, i)
        busyThreads = []
        self.result = []
        currentTime = 0
        for jobIndex in range(len(self.jobs)):
            job = self.jobs[jobIndex]
            if (len(availableThreads) == 0):
                releaseTime, releasedThread = heapq.heappop(busyThreads)
                currentTime = releaseTime
                heapq.heappush(availableThreads, releasedThread)

            endTime = currentTime + job
            thread = 0
            if job > 0:
                thread = heapq.heappop(availableThreads)
                heapq.heappush(busyThreads, (endTime, thread))
            else:
                thread = availableThreads[0]

            self.result.append((thread, currentTime))
        return self.result
            
    def solve(self):
        self.read_data()
        self.assign_jobs()
        self.write_response()

if __name__ == '__main__':
    job_queue = JobQueue()
    job_queue.solve()